﻿namespace CarZone.Server.Features.CarExteriors
{
    using System;
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

        public async Task<string> CreateAsync(string carId, string exteriorId)
        {
            var carExterior = new CarExterior
            {
                CarId = carId,
                ExteriorId = exteriorId,
            };

            await this.dbContext.CarExteriors.AddAsync(carExterior);
            await this.dbContext.SaveChangesAsync();

            return carExterior.Id;
        }

        public async Task DeleteAsync(CarExterior carExterior)
        {
            carExterior.IsDeleted = true;
            carExterior.DeletedOn = DateTime.UtcNow;

            this.dbContext.CarExteriors.Update(carExterior);
            await this.dbContext.SaveChangesAsync();
        }
    }
}