namespace CarZone.Server.Features.Advertisements
{
    using System;
    using System.Linq;
    using System.Security.Cryptography.Xml;
    using System.Threading.Tasks;

    using CarZone.Server.Data;
    using CarZone.Server.Data.Models;
    using CarZone.Server.Features.Advertisements.Models;
    using CarZone.Server.Features.Cars;
    using CarZone.Server.Features.Common.Models;
    using CarZone.Server.Features.Images;
    using CarZone.Server.Features.Users;
    using Microsoft.EntityFrameworkCore;

    using static CarZone.Server.Features.Common.Constants;

    public class AdvertisementsService : IAdvertisementsService
    {
        private readonly CarZoneDbContext dbContext;
        private readonly IUsersService usersService;
        private readonly ICarsService carsService;
        private readonly IImagesService imagesService;

        public AdvertisementsService(
            CarZoneDbContext dbContext,
            IUsersService usersService,
            ICarsService carsService,
            IImagesService imagesService)
        {
            this.dbContext = dbContext;
            this.usersService = usersService;
            this.carsService = carsService;
            this.imagesService = imagesService;
        }

        public async Task<string> CreateAsync(string userId, CreateAdvertisementRequestModel input)
        {
            input.Car.OwnerId = userId;

            var newCarId = await this.carsService.CreateAsync(input.Car);

            var newAdvertisement = new Advertisement
            {
                Title = input.Title,
                Description = input.Description,
                CarId = newCarId,
                AuthorId = userId,
            };

            await this.dbContext.Advertisements.AddAsync(newAdvertisement);
            await this.dbContext.SaveChangesAsync();

            foreach (var url in input.ImageURLs)
            {
                await this.imagesService.CreateAsync(url, newAdvertisement.Id);
            }

            await this.carsService.SetCarsAdvertisementAsync(newCarId, newAdvertisement.Id);

            return newAdvertisement.Id;
        }

        public async Task<ResultModel<bool>> DeleteAsync(string userId, string advertisementId)
        {
            var advertisement = await this.GetByIdAsync(advertisementId);

            if (advertisement == null)
            {
                return new ResultModel<bool>
                {
                    Errors = new string[] { Errors.InvalidAdvertisementId },
                };
            }

            if (userId == advertisement.AuthorId || await this.usersService.IsAdminAsync(userId))
            {
                var deleteCarRequest = await this.carsService.DeleteAsync(advertisement.CarId);

                if (!deleteCarRequest.Success)
                {
                    return new ResultModel<bool>
                    {
                        Errors = deleteCarRequest.Errors
                    };
                }

                await this.imagesService.DeleteAllByAdvertisementIdAsync(advertisementId);

                advertisement.IsDeleted = true;
                advertisement.DeletedOn = DateTime.UtcNow;

                this.dbContext.Advertisements.Update(advertisement);
                await this.dbContext.SaveChangesAsync();

                return new ResultModel<bool>
                {
                    Success = true,
                };
            }

            return new ResultModel<bool>
            {
                Errors = new string[] { Errors.NoPermissionToDeleteAdvertisement },
            };
        }

        private async Task<Advertisement> GetByIdAsync(string id)
        {
            return await this.dbContext
                .Advertisements
                .Include(a => a.Images)
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
