﻿namespace CarZone.Server.Features.CarComforts
{
    using System.Threading.Tasks;

    using CarZone.Server.Data;
    using CarZone.Server.Data.Models.Comfort;

    public class CarComfortsService : ICarComfortsService
    {
        private readonly CarZoneDbContext dbContext;

        public CarComfortsService(CarZoneDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> CreateAsync(string carId, string comfortId)
        {
            var carComfort = new CarComfort
            {
                CarId = carId,
                ComfortId = comfortId,
            };

            await this.dbContext.CarComforts.AddAsync(carComfort);

            await this.dbContext.SaveChangesAsync();

            return carComfort.Id;
        }
    }
}
