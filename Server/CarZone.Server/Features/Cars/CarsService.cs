namespace CarZone.Server.Features.Cars
{
    using System.Linq;
    using System.Threading.Tasks;

    using CarZone.Server.Data;
    using CarZone.Server.Data.Models;
    using CarZone.Server.Features.Cars.Models;
    using Microsoft.EntityFrameworkCore;

    public class CarsService : ICarsService
    {
        private readonly CarZoneDbContext dbContext;

        public CarsService(CarZoneDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> CreateAsync(CreateCarRequestModel input)
        {
            var car = new Car
            {
                Price = input.Price,
                FuelType = input.FuelType,
                HorsePower = input.HorsePower,
                Transmission = input.Transmission,
                Year = input.Year,
                Mileage = input.Mileage,
                Color = input.Color,
                Condition = input.Condition,
                EuroStandard = input.EuroStandard,
                DoorsCount = input.DoorsCount,
                BodyType = input.BodyType,
                BrandId = input.BrandId,
                ModelId = input.ModelId,
                OwnerId = input.OwnerId,
            };

            await this.dbContext.Cars.AddAsync(car);
            await this.dbContext.SaveChangesAsync();

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
