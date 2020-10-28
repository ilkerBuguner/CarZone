namespace CarZone.Server.Features.Images
{
    using System.Threading.Tasks;
    using CarZone.Server.Data;
    using CarZone.Server.Data.Models;

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
    }
}
