namespace CarZone.Server.Features.CarSafeties
{
    using System.Threading.Tasks;

    using CarZone.Server.Data;
    using CarZone.Server.Data.Models.Safety;

    public class CarSafetiesService : ICarSafetiesService
    {
        private readonly CarZoneDbContext dbContext;

        public CarSafetiesService(CarZoneDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> Create(string carId, string safetyId)
        {
            var carSafety = new CarSafety
            {
                CarId = carId,
                SafetyId = safetyId,
            };

            await this.dbContext.AddAsync(carSafety);
            await this.dbContext.SaveChangesAsync();

            return carSafety.Id;
        }
    }
}
