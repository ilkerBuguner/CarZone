namespace CarZone.Server.Features.CarComforts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CarZone.Server.Data;
    using CarZone.Server.Data.Models.Comfort;
    using CarZone.Server.Features.Common.Models;
    using Microsoft.EntityFrameworkCore;

    using static CarZone.Server.Features.Common.Constants;

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

        public async Task<ResultModel<bool>> DeleteAsync(string carId, string comfortId)
        {
            var carComfort = await this.GetByIdsAsync(carId, comfortId);

            if (carComfort == null)
            {
                return new ResultModel<bool>
                {
                    Errors = new string[] { Errors.InvalidIdsForCarComfort },
                };
            }

            carComfort.IsDeleted = true;
            carComfort.DeletedOn = DateTime.UtcNow;

            this.dbContext.CarComforts.Update(carComfort);
            await this.dbContext.SaveChangesAsync();

            return new ResultModel<bool>
            {
                Success = true,
            };
        }

        public async Task<ResultModel<bool>> DeleteAllByCarIdAsync(string carId)
        {
            var carComforts = await this.GetAllByCarIdAsync(carId);

            foreach (var carComfort in carComforts)
            {
                var carComfortDeleteRequest = await this.DeleteAsync(carComfort.CarId, carComfort.ComfortId);

                if (!carComfortDeleteRequest.Success)
                {
                    return new ResultModel<bool>
                    {
                        Errors = carComfortDeleteRequest.Errors,
                    };
                }
            }

            return new ResultModel<bool>
            {
                Success = true,
            };
        }

        private async Task<CarComfort> GetByIdsAsync(string carId, string comfortId)
        {
            return await this.dbContext
                .CarComforts
                .Where(cc => cc.CarId == carId && cc.ComfortId == comfortId)
                .FirstOrDefaultAsync();
        }

        private async Task<ICollection<CarComfort>> GetAllByCarIdAsync(string carId)
        {
            return await this.dbContext
                .CarComforts
                .Where(cc => cc.CarId == carId)
                .ToListAsync();
        }
    }
}
