namespace CarZone.Server.Features.Cars
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CarZone.Server.Data;
    using CarZone.Server.Data.Enumerations;
    using CarZone.Server.Data.Models;
    using CarZone.Server.Features.CarComforts;
    using CarZone.Server.Features.CarExteriors;
    using CarZone.Server.Features.CarProtections;
    using CarZone.Server.Features.Cars.Models;
    using CarZone.Server.Features.CarSafeties;
    using CarZone.Server.Features.Common.Models;
    using CarZone.Server.Features.Users;
    using Microsoft.EntityFrameworkCore;

    using static CarZone.Server.Features.Common.Constants;

    public class CarsService : ICarsService
    {
        private readonly CarZoneDbContext dbContext;
        private readonly IUsersService usersService;
        private readonly ICarComfortsService carComfortsService;
        private readonly ICarExteriorsService carExteriorsService;
        private readonly ICarProtectionsService carProtectionsService;
        private readonly ICarSafetiesService carSafetiesService;

        public CarsService(
            CarZoneDbContext dbContext,
            IUsersService usersService,
            ICarComfortsService carComfortsService,
            ICarExteriorsService carExteriorsService,
            ICarProtectionsService carProtectionsService,
            ICarSafetiesService carSafetiesService)
        {
            this.dbContext = dbContext;
            this.usersService = usersService;
            this.carComfortsService = carComfortsService;
            this.carExteriorsService = carExteriorsService;
            this.carProtectionsService = carProtectionsService;
            this.carSafetiesService = carSafetiesService;
        }

        public async Task<string> CreateAsync(CreateCarRequestModel input)
        {
            var car = new Car
            {
                Price = input.Price,
                FuelType = (FuelType)Enum.Parse(typeof(FuelType), input.FuelType),
                HorsePower = input.HorsePower,
                Transmission = (TransmissionType)Enum.Parse(typeof(TransmissionType), input.Transmission),
                Year = input.Year,
                Mileage = input.Mileage,
                Color = (Color)Enum.Parse(typeof(Color), input.Color),
                Condition = (ConditionType)Enum.Parse(typeof(ConditionType), input.Condition),
                EuroStandard = (EuroStandard)Enum.Parse(typeof(EuroStandard), input.EuroStandard),
                DoorsCount = (DoorsCount)Enum.Parse(typeof(DoorsCount), input.DoorsCount),
                BodyType = (BodyType)Enum.Parse(typeof(BodyType), input.BodyType),
                BrandId = input.BrandId,
                ModelId = input.ModelId,
                OwnerId = input.OwnerId,
            };

            await this.dbContext.Cars.AddAsync(car);
            await this.dbContext.SaveChangesAsync();

            foreach (var comfortId in input.Comforts)
            {
                await this.carComfortsService.CreateAsync(car.Id, comfortId);
            }

            foreach (var exteriorId in input.Exteriors)
            {
                await this.carExteriorsService.CreateAsync(car.Id, exteriorId);
            }

            foreach (var protectionId in input.Protections)
            {
                await this.carProtectionsService.CreateAsync(car.Id, protectionId);
            }

            foreach (var safetyId in input.Safeties)
            {
                await this.carSafetiesService.CreateAsync(car.Id, safetyId);
            }

            return car.Id;
        }

        public async Task<ResultModel<bool>> UpdateAsync(string userId, string carId, UpdateCarRequestModel model)
        {
            var car = await this.GetByIdAsync(carId);

            if (car == null)
            {
                return new ResultModel<bool>
                {
                    Errors = new string[] { Errors.InvalidCarId },
                };
            }

            if (car.OwnerId == userId || await this.usersService.IsAdminAsync(userId))
            {
                car.Price = model.Price;
                car.HorsePower = model.HorsePower;
                car.Year = model.Year;
                car.Mileage = model.Mileage;
                car.BrandId = model.BrandId;
                car.ModelId = model.ModelId;
                car.FuelType = (FuelType)Enum.Parse(typeof(FuelType), model.FuelType);
                car.Transmission = (TransmissionType)Enum.Parse(typeof(TransmissionType), model.Transmission);
                car.Color = (Color)Enum.Parse(typeof(Color), model.Color);
                car.Condition = (ConditionType)Enum.Parse(typeof(ConditionType), model.Condition);
                car.EuroStandard = (EuroStandard)Enum.Parse(typeof(EuroStandard), model.EuroStandard);
                car.DoorsCount = (DoorsCount)Enum.Parse(typeof(DoorsCount), model.DoorsCount);
                car.BodyType = (BodyType)Enum.Parse(typeof(BodyType), model.BodyType);
                car.ModifiedOn = DateTime.UtcNow;

                foreach (var carComfort in model.CarComforts)
                {
                    carComfort.CarId = car.Id;
                    await this.carComfortsService.UpdateAsync(carComfort);
                }

                foreach (var carExterior in model.CarExteriors)
                {
                    carExterior.CarId = car.Id;
                    await this.carExteriorsService.UpdateAsync(carExterior);
                }

                foreach (var carProtection in model.CarProtections)
                {
                    carProtection.CarId = car.Id;
                    await this.carProtectionsService.UpdateAsync(carProtection);
                }

                foreach (var carSafety in model.CarSafeties)
                {
                    carSafety.CarId = car.Id;
                    await this.carSafetiesService.UpdateAsync(carSafety);
                }

                this.dbContext.Cars.Update(car);
                await this.dbContext.SaveChangesAsync();

                return new ResultModel<bool>
                {
                    Success = true,
                };
            }

            return new ResultModel<bool>
            {
                Errors = new string[] { Errors.NoPermissionToEditCar },
            };
        }

        public async Task<ResultModel<bool>> DeleteAsync(string id)
        {
            var car = await this.GetByIdAsync(id);

            if (car == null)
            {
                return new ResultModel<bool>
                {
                    Errors = new string[] { Errors.InvalidCarId },
                };
            }

            await this.carComfortsService.DeleteAllByCarIdAsync(id);
            await this.carExteriorsService.DeleteAllByCarIdAsync(id);
            await this.carProtectionsService.DeleteAllByCarIdAsync(id);
            await this.carSafetiesService.DeleteAllByCarIdAsync(id);

            car.IsDeleted = true;
            car.DeletedOn = DateTime.UtcNow;

            this.dbContext.Cars.Update(car);
            await this.dbContext.SaveChangesAsync();

            return new ResultModel<bool>
            {
                Success = true,
            };
        }

        public async Task SetCarsAdvertisementAsync(string carId, string advertisementId)
        {
            var car = await this.dbContext
                .Cars
                .Where(c => c.Id == carId)
                .FirstOrDefaultAsync();

            car.AdvertisementId = advertisementId;

            this.dbContext.Cars.Update(car);
            await this.dbContext.SaveChangesAsync();
        }

        private async Task<Car> GetByIdAsync(string id)
        {
            return await this.dbContext
                .Cars
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
