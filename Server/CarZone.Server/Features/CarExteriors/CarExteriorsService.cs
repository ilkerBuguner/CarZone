namespace CarZone.Server.Features.CarExteriors
{
    using System.Threading.Tasks;

    using CarZone.Server.Data;
    using CarZone.Server.Data.Models.Exterior;

    public class CarExteriorsService : ICarExteriorsService
    {
        private readonly CarZoneDbContext dbContext;

        public CarExteriorsService(CarZoneDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> Create(string carId, string exteriorId)
        {
            var carExterior = new CarExterior
            {
                CarId = carId,
                ExteriorId = exteriorId,
            };

            await this.dbContext.AddAsync(carExterior);
            await this.dbContext.SaveChangesAsync();

            return carExterior.Id;
        }
    }
}
