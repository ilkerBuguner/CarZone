namespace CarZone.Server.Features.CarProtections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CarZone.Server.Data;
    using CarZone.Server.Data.Models.Protection;
    using CarZone.Server.Features.Common.Models;
    using Microsoft.EntityFrameworkCore;

    using static CarZone.Server.Features.Common.Constants;

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

        public async Task<ResultModel<bool>> DeleteAsync(string carId, string protectionId)
        {
            var carProtection = await this.GetByIdsAsync(carId, protectionId);

            if (carProtection == null)
            {
                return new ResultModel<bool>
                {
                    Errors = new string[] { Errors.InvalidIdsForCarProtection },
                };
            }

            carProtection.IsDeleted = true;
            carProtection.DeletedOn = DateTime.UtcNow;

            this.dbContext.CarProtections.Update(carProtection);
            await this.dbContext.SaveChangesAsync();

            return new ResultModel<bool>
            {
                Success = true,
            };
        }

        public async Task<ResultModel<bool>> DeleteAllByCarIdAsync(string carId)
        {
            var carProtections = await this.GetAllByCarIdAsync(carId);

            foreach (var carProtection in carProtections)
            {
                var carProtectionDeleteRequest = await this.DeleteAsync(carProtection.CarId, carProtection.ProtectionId);

                if (!carProtectionDeleteRequest.Success)
                {
                    return new ResultModel<bool>
                    {
                        Errors = carProtectionDeleteRequest.Errors,
                    };
                }
            }

            return new ResultModel<bool>
            {
                Success = true,
            };
        }

        public async Task<CarProtection> GetByIdsAsync(string carId, string protectionId)
        {
            return await this.dbContext
                .CarProtections
                .Where(cp => cp.CarId == carId && cp.ProtectionId == protectionId)
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<CarProtection>> GetAllByCarIdAsync(string carId)
        {
            return await this.dbContext
                .CarProtections
                .Where(cp => cp.CarId == carId)
                .ToListAsync();
        }
    }
}
