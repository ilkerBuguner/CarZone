namespace CarZone.Server.Features.CarComforts
{
    using CarZone.Server.Data;
    using CarZone.Server.Data.Models.Comfort;
    using System.Threading.Tasks;

    public class CarComfortsService : ICarComfortsService
    {
        private readonly CarZoneDbContext dbContext;

        public CarComfortsService(CarZoneDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> Create(string carId, string comfortId)
        {
            var carComfort = new CarComfort
            {
                CarId = carId,
                ComfortId = comfortId,
            };

            this.dbContext.Add(carComfort);

            await this.dbContext.SaveChangesAsync();

            return carComfort.Id;
        }
    }
}
