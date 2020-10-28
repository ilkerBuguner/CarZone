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
    using Microsoft.EntityFrameworkCore;

    public class CarsService : ICarsService
    {
        private readonly CarZoneDbContext dbContext;
        private readonly ICarComfortsService carComfortsService;
        private readonly ICarExteriorsService carExteriorsService;
        private readonly ICarProtectionsService carProtectionsService;
        private readonly ICarSafetiesService carSafetiesService;

        public CarsService(
            CarZoneDbContext dbContext,
            ICarComfortsService carComfortsService,
            ICarExteriorsService carExteriorsService,
            ICarProtectionsService carProtectionsService,
            ICarSafetiesService carSafetiesService)
        {
            this.dbContext = dbContext;
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
    }
}
