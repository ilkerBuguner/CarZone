namespace CarZone.Server.Features.Images
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CarZone.Server.Data;
    using CarZone.Server.Data.Models;
    using CarZone.Server.Features.Common.Models;
    using Microsoft.EntityFrameworkCore;

    using static CarZone.Server.Features.Common.Constants;

    public class ImagesService : IImagesService
    {
        private readonly CarZoneDbContext dbContext;

        public ImagesService(CarZoneDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> CreateAsync(string url, string advertisementId)
        {
            var image = new Image
            {
                Url = url,
                AdvertisementId = advertisementId,
            };

            await this.dbContext.Images.AddAsync(image);
            await this.dbContext.SaveChangesAsync();

            return image.Id;
        }

        public async Task<ResultModel<bool>> DeleteAsync(string id)
        {
            var image = await this.GetByIdAsync(id);

            if (image == null)
            {
                return new ResultModel<bool>
                {
                    Errors = new string[] { Errors.InvalidImageId },
                };
            }

            image.IsDeleted = true;
            image.DeletedOn = DateTime.UtcNow;

            this.dbContext.Images.Update(image);
            await this.dbContext.SaveChangesAsync();

            return new ResultModel<bool>
            {
                Success = true,
            };
        }

        public async Task DeleteAllByAdvertisementIdAsync(string advertisementId)
        {
            var images = await this.GetAllByAdvertisementIdAsync(advertisementId);

            foreach (var image in images)
            {
                image.IsDeleted = true;
                image.DeletedOn = DateTime.UtcNow;

                this.dbContext.Images.Update(image);
            }

            await this.dbContext.SaveChangesAsync();
        }

        private async Task<Image> GetByIdAsync(string id)
        {
            return await this.dbContext
                .Images
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        private async Task<ICollection<Image>> GetAllByAdvertisementIdAsync(string advertisementId)
        {
            return await this.dbContext
                .Images
                .Where(i => i.AdvertisementId == advertisementId)
                .ToListAsync();
        }
    }
}
