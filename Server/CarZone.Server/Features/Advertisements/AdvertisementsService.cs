namespace CarZone.Server.Features.Advertisements
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography.Xml;
    using System.Threading.Tasks;

    using CarZone.Server.Data;
    using CarZone.Server.Data.Enumerations;
    using CarZone.Server.Data.Models;
    using CarZone.Server.Features.Advertisements.Models;
    using CarZone.Server.Features.BrandModels.Models;
    using CarZone.Server.Features.Brands.Models;
    using CarZone.Server.Features.Cars;
    using CarZone.Server.Features.Cars.Models;
    using CarZone.Server.Features.Common.Models;
    using CarZone.Server.Features.Images;
    using CarZone.Server.Features.Images.Models;
    using CarZone.Server.Features.Users;
    using CarZone.Server.Features.Users.Models;
    using Microsoft.EntityFrameworkCore;

    using static CarZone.Server.Features.Common.Constants;

    public class AdvertisementsService : IAdvertisementsService
    {
        private readonly CarZoneDbContext dbContext;
        private readonly IUsersService usersService;
        private readonly ICarsService carsService;
        private readonly IImagesService imagesService;

        public AdvertisementsService(
            CarZoneDbContext dbContext,
            IUsersService usersService,
            ICarsService carsService,
            IImagesService imagesService)
        {
            this.dbContext = dbContext;
            this.usersService = usersService;
            this.carsService = carsService;
            this.imagesService = imagesService;
        }

        public async Task<string> CreateAsync(string userId, CreateAdvertisementRequestModel input)
        {
            input.Car.OwnerId = userId;

            var newCarId = await this.carsService.CreateAsync(input.Car);

            var newAdvertisement = new Advertisement
            {
                Title = input.Title,
                Description = input.Description,
                CarId = newCarId,
                AuthorId = userId,
            };

            await this.dbContext.Advertisements.AddAsync(newAdvertisement);
            await this.dbContext.SaveChangesAsync();

            foreach (var url in input.ImageURLs)
            {
                await this.imagesService.CreateAsync(url, newAdvertisement.Id);
            }

            await this.carsService.SetCarsAdvertisementAsync(newCarId, newAdvertisement.Id);

            return newAdvertisement.Id;
        }

        public async Task<ResultModel<bool>> DeleteAsync(string userId, string advertisementId)
        {
            var advertisement = await this.GetByIdAsync(advertisementId);

            if (advertisement == null)
            {
                return new ResultModel<bool>
                {
                    Errors = new string[] { Errors.InvalidAdvertisementId },
                };
            }

            if (userId == advertisement.AuthorId || await this.usersService.IsAdminAsync(userId))
            {
                var deleteCarRequest = await this.carsService.DeleteAsync(advertisement.CarId);

                if (!deleteCarRequest.Success)
                {
                    return new ResultModel<bool>
                    {
                        Errors = deleteCarRequest.Errors
                    };
                }

                await this.imagesService.DeleteAllByAdvertisementIdAsync(advertisementId);

                advertisement.IsDeleted = true;
                advertisement.DeletedOn = DateTime.UtcNow;

                this.dbContext.Advertisements.Update(advertisement);
                await this.dbContext.SaveChangesAsync();

                return new ResultModel<bool>
                {
                    Success = true,
                };
            }

            return new ResultModel<bool>
            {
                Errors = new string[] { Errors.NoPermissionToDeleteAdvertisement },
            };
        }

        public async Task<ICollection<AdvertisementListingServiceModel>> GetLatestAsync()
        {
            return await this.dbContext
                .Advertisements
                .Select(a => new AdvertisementListingServiceModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    Views = a.Views,
                    CreatedOn = a.CreatedOn.ToString(),
                    Location = a.Author.Location != null ? a.Author.Location.ToString() : null,
                    ImageUrl = a.Images.Select(x => x.Url).FirstOrDefault(),
                    Author = new UserInfoServiceModel
                    {
                        Id = a.Author.Id,
                        Username = a.Author.UserName,
                        ProfilePictureUrl = a.Author.ProfilePictureUrl
                    },
                    Car = new CarInfoServiceModel
                    {
                        Price = a.Car.Price,
                        Year = a.Car.Year,
                        HorsePower = a.Car.HorsePower,
                        BodyType = a.Car.BodyType.ToString(),
                        FuelType = a.Car.FuelType.ToString(),
                    }
                })
                .OrderByDescending(a => a.CreatedOn)
                .ToListAsync();
        }

        public async Task<ResultModel<AdvertisementDetailsServiceModel>> DetailsAsync(string id)
        {
            var advertisement = await this.dbContext.
                Advertisements
                .Where(a => a.Id == id)
                .Select(a => new AdvertisementDetailsServiceModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    Views = a.Views,
                    Location = a.Author.Location != null ? a.Author.Location.ToString() : null,
                    Images = a.Images.Select(i => new ImageInfoServiceModel
                    {
                        Id = i.Id,
                        Url = i.Url,
                    }).ToList(),
                    Author = new UserInfoServiceModel
                    {
                        Id = a.Author.Id,
                        Username = a.Author.UserName,
                        ProfilePictureUrl = a.Author.ProfilePictureUrl
                    },
                    Car = new CarDetailsServiceModel
                    {
                        Id = a.Car.Id,
                        Price = a.Car.Price,
                        Year = a.Car.Year,
                        HorsePower = a.Car.HorsePower,
                        Mileage = a.Car.Mileage,
                        Color = a.Car.Color.ToString(),
                        FuelType = a.Car.FuelType.ToString(),
                        BodyType = a.Car.BodyType.ToString(),
                        Condition = a.Car.Condition.ToString(),
                        DoorsCount = a.Car.DoorsCount.ToString(),
                        Transmission = a.Car.Transmission.ToString(),
                        EuroStandard = a.Car.EuroStandard.ToString(),
                        Model = new BrandModelInfoServiceModel
                        {
                            Id = a.Car.Model.Id,
                            Name = a.Car.Model.Name,
                        },
                        Brand = new BrandInfoServiceModel
                        {
                            Id = a.Car.Brand.Id,
                            Name = a.Car.Brand.Name
                        },
                        Comforts = a.Car.Comforts.Select(c => c.Comfort.Name).ToList(),
                        Exteriors = a.Car.Exteriors.Select(e => e.Exterior.Name).ToList(),
                        Protections = a.Car.Protections.Select(p => p.Protection.Name).ToList(),
                        Safeties = a.Car.Safeties.Select(s => s.Safety.Name).ToList(),
                    }
                })
                .FirstOrDefaultAsync();

            if (advertisement == null)
            {
                return new ResultModel<AdvertisementDetailsServiceModel>
                {
                    Errors = new string[] { Errors.InvalidAdvertisementId },
                };
            }

            return new ResultModel<AdvertisementDetailsServiceModel>
            {
                Success = true,
                Result = advertisement,
            };
        }

        public async Task<ICollection<AdvertisementListingServiceModel>> GetBySearchAsync(CarSearchRequestModel model)
        {
            var cars = await this.dbContext.Cars
                .Include(c => c.Advertisement)
                .Include(c => c.Owner)
                .Include(c => c.Advertisement.Images)
                .Include(c => c.Brand)
                .Include(c => c.Model)
                .ToListAsync();

            if (model.MinPrice != 0 && model.MaxPrice != 0)
            {
                cars = cars.Where(c => c.Price > model.MinPrice && c.Price < model.MaxPrice).ToList();
            }
            else if (model.MinPrice != 0)
            {
                cars = cars.Where(c => c.Price > model.MinPrice).ToList();
            }
            else if (model.MaxPrice != 0)
            {
                cars = cars.Where(c => c.Price < model.MaxPrice).ToList();
            }

            if (model.MinYear != 0 && model.MaxYear != 0)
            {
                cars = cars.Where(c => c.Year > model.MinYear && c.Year < model.MaxYear).ToList();
            }
            else if (model.MinYear != 0)
            {
                cars = cars.Where(c => c.Year > model.MinYear).ToList();
            }
            else if (model.MaxYear != 0)
            {
                cars = cars.Where(c => c.Year < model.MaxYear).ToList();
            }

            // cars = model.MinYear != 0 && model.MaxYear != 0 ? cars = cars.Where(a => a.Year > model.MinYear && a.Year < model.MaxYear).ToList()
            //    : model.MinYear != 0 ? cars.Where(a => a.Year > model.MinYear).ToList()
            //    : model.MaxYear != 0 ? cars.Where(a => a.Year < model.MaxYear).ToList();

            if (model.MinHorsePower != 0 && model.MaxHorsePower != 0)
            {
                cars = cars.Where(c => c.HorsePower > model.MinHorsePower && c.HorsePower < model.MaxHorsePower).ToList();
            }
            else if (model.MinHorsePower != 0)
            {
                cars = cars.Where(c => c.HorsePower > model.MinHorsePower).ToList();
            }
            else if (model.MaxHorsePower != 0)
            {
                cars = cars.Where(c => c.HorsePower < model.MaxHorsePower).ToList();
            }

            if (model.FuelType != null)
            {
                var fuelType = (FuelType)Enum.Parse(typeof(FuelType), model.FuelType);

                cars = cars.Where(c => c.FuelType == fuelType).ToList();
            }

            if (model.Transmission != null)
            {
                var transmissionType = (TransmissionType)Enum.Parse(typeof(TransmissionType), model.Transmission);

                cars = cars.Where(c => c.Transmission == transmissionType).ToList();
            }

            if (model.Color != null)
            {
                var color = (Color)Enum.Parse(typeof(Color), model.Color);

                cars = cars.Where(c => c.Color == color).ToList();
            }

            if (model.Condition != null)
            {
                var condition = (ConditionType)Enum.Parse(typeof(ConditionType), model.Condition);

                cars = cars.Where(c => c.Condition == condition).ToList();
            }

            if (model.EuroStandard != null)
            {
                var euroStandard = (EuroStandard)Enum.Parse(typeof(EuroStandard), model.EuroStandard);

                cars = cars.Where(c => c.EuroStandard == euroStandard).ToList();
            }

            if (model.DoorsCount != null)
            {
                var doorsCount = (DoorsCount)Enum.Parse(typeof(DoorsCount), model.DoorsCount);

                cars = cars.Where(c => c.DoorsCount == doorsCount).ToList();
            }

            if (model.BodyType != null)
            {
                var bodyType = (BodyType)Enum.Parse(typeof(BodyType), model.BodyType);

                cars = cars.Where(c => c.BodyType == bodyType).ToList();
            }

            if (model.BrandName != null)
            {
                cars = cars.Where(c => c.Brand.Name == model.BrandName).ToList();
            }

            if (model.ModelName != null)
            {
                cars = cars.Where(c => c.Model.Name == model.ModelName).ToList();
            }

            var result = cars
                .Select(c => new AdvertisementListingServiceModel
                {
                    Id = c.Advertisement.Id,
                    Title = c.Advertisement.Title,
                    Views = c.Advertisement.Views,
                    CreatedOn = c.Advertisement.CreatedOn.ToString(),
                    Location = c.Owner.Location != null ? c.Owner.Location.ToString() : null,
                    ImageUrl = c.Advertisement.Images.Select(x => x.Url).FirstOrDefault(),
                    Author = new UserInfoServiceModel
                    {
                        Id = c.Owner.Id,
                        Username = c.Owner.UserName,
                        ProfilePictureUrl = c.Owner.ProfilePictureUrl
                    },
                    Car = new CarInfoServiceModel
                    {
                        Price = c.Price,
                        Year = c.Year,
                        HorsePower = c.HorsePower,
                        BodyType = c.BodyType.ToString(),
                        FuelType = c.FuelType.ToString(),
                    }
                })
                .OrderByDescending(c => c.CreatedOn)
                .ToList();

            return result;
        }

        private async Task<Advertisement> GetByIdAsync(string id)
        {
            return await this.dbContext
                .Advertisements
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
