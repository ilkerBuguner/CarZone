namespace CarZone.Server.Features.CarSafeties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CarZone.Server.Data;
    using CarZone.Server.Data.Models.Safety;
    using CarZone.Server.Features.Common.Models;
    using Microsoft.EntityFrameworkCore;

    using static CarZone.Server.Features.Common.Constants;

    public class CarSafetiesService : ICarSafetiesService
    {
        private readonly CarZoneDbContext dbContext;

        public CarSafetiesService(CarZoneDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> CreateAsync(string carId, string safetyId)
        {
            var carSafety = new CarSafety
            {
                CarId = carId,
                SafetyId = safetyId,
            };

            await this.dbContext.CarSafeties.AddAsync(carSafety);
            await this.dbContext.SaveChangesAsync();

            return carSafety.Id;
        }

        public async Task<ResultModel<bool>> DeleteAsync(string carId, string safetyId)
        {
            var carSafety = await this.GetByIdsAsync(carId, safetyId);

            if (carSafety == null)
            {
                return new ResultModel<bool>
                {
                    Errors = new string[] { Errors.InvalidIdsForCarSafety },
                };
            }

            carSafety.IsDeleted = true;
            carSafety.DeletedOn = DateTime.UtcNow;

            this.dbContext.CarSafeties.Update(carSafety);
            await this.dbContext.SaveChangesAsync();

            return new ResultModel<bool>
            {
                Success = true,
            };
        }

        public async Task<ResultModel<bool>> DeleteAllByCarIdAsync(string carId)
        {
            var carSafeties = await this.GetAllByCarIdAsync(carId);

            foreach (var carSafety in carSafeties)
            {
                var carSafetyDeleteRequest = await this.DeleteAsync(carSafety.CarId, carSafety.SafetyId);

                if (!carSafetyDeleteRequest.Success)
                {
                    return new ResultModel<bool>
                    {
                        Errors = carSafetyDeleteRequest.Errors,
                    };
                }
            }

            return new ResultModel<bool>
            {
                Success = true,
            };
        }

        private async Task<CarSafety> GetByIdsAsync(string carId, string safetyId)
        {
            return await this.dbContext
                .CarSafeties
                .Where(cs => cs.CarId == carId && cs.SafetyId == safetyId)
                .FirstOrDefaultAsync();
        }

        private async Task<ICollection<CarSafety>> GetAllByCarIdAsync(string carId)
        {
            return await this.dbContext
                .CarSafeties
                .Where(cs => cs.CarId == carId)
                .ToListAsync();
        }
    }
}
