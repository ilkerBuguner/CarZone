using CarZone.Server.Data;
using CarZone.Server.Data.Models.Protection;
using System.Threading.Tasks;

namespace CarZone.Server.Features.CarProtections
{
    public class CarProtectionsService : ICarProtectionsService
    {
        private readonly CarZoneDbContext dbContext;

        public CarProtectionsService(CarZoneDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> Create(string carId, string protectionId)
        {
            var carProtection = new CarProtection
            {
                CarId = carId,
                ProtectionId = protectionId,
            };

            await this.dbContext.AddAsync(carProtection);
            await this.dbContext.SaveChangesAsync();

            return carProtection.Id;
        }
    }
}
