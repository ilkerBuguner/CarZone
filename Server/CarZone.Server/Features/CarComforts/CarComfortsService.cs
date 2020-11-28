namespace CarZone.Server.Features.CarComforts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CarZone.Server.Data;
    using CarZone.Server.Data.Models.Comfort;
    using CarZone.Server.Features.CarComforts.Models;
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

        public async Task UpdateAsync(UpdateCarComfortRequestModel model)
        {
            var carComfort = await this.GetByIdsAsync(model.CarId, model.Id);

            if (model.IsChecked == true)
            {
                if (carComfort == null)
                {
                    var deepSearchedCarComfort = await this.dbContext.CarComforts
                        .IgnoreQueryFilters()
                        .Where(cc => cc.CarId == model.CarId
                            && cc.ComfortId == model.Id
                            && cc.IsDeleted == true)
                        .FirstOrDefaultAsync();

                    if (deepSearchedCarComfort == null)
                    {
                        await this.CreateAsync(model.CarId, model.Id);
                    }
                    else
                    {
                        deepSearchedCarComfort.IsDeleted = false;
                        deepSearchedCarComfort.DeletedOn = null;

                        this.dbContext.CarComforts.Update(deepSearchedCarComfort);
                        await this.dbContext.SaveChangesAsync();
                    }
                }
            }
            else if (model.IsChecked == false)
            {
                if (carComfort != null)
                {
                    await this.DeleteAsync(model.CarId, model.Id);
                }
            }
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

        public async Task DeleteAllByCarIdAsync(string carId)
        {
            var carComforts = await this.GetAllByCarIdAsync(carId);

            foreach (var carComfort in carComforts)
            {
                carComfort.IsDeleted = true;
                carComfort.DeletedOn = DateTime.UtcNow;

                this.dbContext.CarComforts.Update(carComfort);
            }

            await this.dbContext.SaveChangesAsync();
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
