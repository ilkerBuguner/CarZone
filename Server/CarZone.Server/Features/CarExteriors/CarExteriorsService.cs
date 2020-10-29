namespace CarZone.Server.Features.CarExteriors
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CarZone.Server.Data;
    using CarZone.Server.Data.Models.Exterior;
    using CarZone.Server.Features.Common.Models;
    using Microsoft.EntityFrameworkCore;

    using static CarZone.Server.Features.Common.Constants;

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

        public async Task<ResultModel<bool>> DeleteAsync(string carId, string exteriorId)
        {
            var carExterior = await this.GetByIdsAsync(carId, exteriorId);

            if (carExterior == null)
            {
                return new ResultModel<bool>
                {
                    Errors = new string[] { Errors.InvalidIdsForCarExterior },
                };
            }

            carExterior.IsDeleted = true;
            carExterior.DeletedOn = DateTime.UtcNow;

            this.dbContext.CarExteriors.Update(carExterior);
            await this.dbContext.SaveChangesAsync();

            return new ResultModel<bool>
            {
                Success = true,
            };
        }

        private async Task<CarExterior> GetByIdsAsync(string carId, string exteriorId)
        {
            return await this.dbContext
                .CarExteriors
                .Where(ce => ce.CarId == carId && ce.ExteriorId == exteriorId)
                .FirstOrDefaultAsync();
        }
    }
}
