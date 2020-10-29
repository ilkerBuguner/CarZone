namespace CarZone.Server.Features.CarProtections
{
    using System;
    using System.Threading.Tasks;

    using CarZone.Server.Data;
    using CarZone.Server.Data.Models.Protection;

    public class CarProtectionsService : ICarProtectionsService
    {
        private readonly CarZoneDbContext dbContext;

        public CarProtectionsService(CarZoneDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> CreateAsync(string carId, string protectionId)
        {
            var carProtection = new CarProtection
            {
                CarId = carId,
                ProtectionId = protectionId,
            };

            await this.dbContext.CarProtections.AddAsync(carProtection);
            await this.dbContext.SaveChangesAsync();

            return carProtection.Id;
        }

        public async Task DeleteAsync(CarProtection carProtection)
        {
            carProtection.IsDeleted = true;
            carProtection.DeletedOn = DateTime.UtcNow;

            this.dbContext.CarProtections.Update(carProtection);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
