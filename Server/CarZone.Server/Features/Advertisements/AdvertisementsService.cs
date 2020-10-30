namespace CarZone.Server.Features.Advertisements
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography.Xml;
    using System.Threading.Tasks;

    using CarZone.Server.Data;
    using CarZone.Server.Data.Models;
    using CarZone.Server.Features.Advertisements.Models;
    using CarZone.Server.Features.BrandModels.Models;
    using CarZone.Server.Features.Brands.Models;
    using CarZone.Server.Features.Cars;
    using CarZone.Server.Features.Cars.Models;
    using CarZone.Server.Features.Common.Models;
    using CarZone.Server.Features.Images;
    using CarZone.Server.Features.Images.Models;
    using CarZone.Server.Features.Users;
    using CarZone.Server.Features.Users.Models;
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

        public async Task<ICollection<AdvertisementListingServiceModel>> GetLatestAsync()
        {
            return await this.dbContext
                .Advertisements
                .Select(a => new AdvertisementListingServiceModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    Views = a.Views,
                    CreatedOn = a.CreatedOn.ToString(),
                    Location = a.Author.Location != null ? a.Author.Location.ToString() : null,
                    ImageUrl = a.Images.Select(x => x.Url).FirstOrDefault(),
                    Author = new UserInfoServiceModel
                    {
                        Id = a.Author.Id,
                        Username = a.Author.UserName,
                        ProfilePictureUrl = a.Author.ProfilePictureUrl
                    },
                    Car = new CarInfoServiceModel
                    {
                        Price = a.Car.Price,
                        Year = a.Car.Year,
                        HorsePower = a.Car.HorsePower,
                        BodyType = a.Car.BodyType.ToString(),
                        FuelType = a.Car.FuelType.ToString(),
                    }
                })
                .OrderByDescending(a => a.CreatedOn)
                .ToListAsync();
        }

        public async Task<ResultModel<AdvertisementDetailsServiceModel>> DetailsAsync(string id)
        {
            var advertisement = await this.dbContext.
                Advertisements
                .Where(a => a.Id == id)
                .Select(a => new AdvertisementDetailsServiceModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    Views = a.Views,
                    Location = a.Author.Location != null ? a.Author.Location.ToString() : null,
                    Images = a.Images.Select(i => new ImageInfoServiceModel
                    {
                        Id = i.Id,
                        Url = i.Url,
                    }).ToList(),
                    Author = new UserInfoServiceModel
                    {
                        Id = a.Author.Id,
                        Username = a.Author.UserName,
                        ProfilePictureUrl = a.Author.ProfilePictureUrl
                    },
                    Car = new CarDetailsServiceModel
                    {
                        Id = a.Car.Id,
                        Price = a.Car.Price,
                        Year = a.Car.Year,
                        HorsePower = a.Car.HorsePower,
                        Mileage = a.Car.Mileage,
                        Color = a.Car.Color.ToString(),
                        FuelType = a.Car.FuelType.ToString(),
                        BodyType = a.Car.BodyType.ToString(),
                        Condition = a.Car.Condition.ToString(),
                        DoorsCount = a.Car.DoorsCount.ToString(),
                        Transmission = a.Car.Transmission.ToString(),
                        EuroStandard = a.Car.EuroStandard.ToString(),
                        Model = new BrandModelInfoServiceModel
                        {
                            Id = a.Car.Model.Id,
                            Name = a.Car.Model.Name,
                        },
                        Brand = new BrandInfoServiceModel
                        {
                            Id = a.Car.Brand.Id,
                            Name = a.Car.Brand.Name
                        },
                        Comforts = a.Car.Comforts.Select(c => c.Comfort.Name).ToList(),
                        Exteriors = a.Car.Exteriors.Select(e => e.Exterior.Name).ToList(),
                        Protections = a.Car.Protections.Select(p => p.Protection.Name).ToList(),
                        Safeties = a.Car.Safeties.Select(s => s.Safety.Name).ToList(),
                    }
                })
                .FirstOrDefaultAsync();

            if (advertisement == null)
            {
                return new ResultModel<AdvertisementDetailsServiceModel>
                {
                    Errors = new string[] { Errors.InvalidAdvertisementId },
                };
            }

            return new ResultModel<AdvertisementDetailsServiceModel>
            {
                Success = true,
                Result = advertisement,
            };
        }

        private async Task<Advertisement> GetByIdAsync(string id)
        {
            return await this.dbContext
                .Advertisements
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
