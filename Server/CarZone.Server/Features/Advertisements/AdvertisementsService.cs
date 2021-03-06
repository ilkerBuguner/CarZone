﻿namespace CarZone.Server.Features.Advertisements
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
            if (!string.IsNullOrEmpty(input.PhoneNumber))
            {
                await this.usersService.SetUserPhoneNumberIfNull(userId, input.PhoneNumber);
            }

            if (!string.IsNullOrEmpty(input.Location))
            {
                await this.usersService.SetUserLocationIfNull(userId, input.Location);
            }

            return newAdvertisement.Id;
        }

        public async Task<ResultModel<bool>> UpdateAsync(string userId, string advertisementId, UpdateAdvertisementRequestModel model)
        {
            var advertisement = await this.GetByIdAsync(advertisementId);

            if (advertisement == null)
            {
                return new ResultModel<bool>
                {
                    Errors = new string[] { Errors.InvalidAdvertisementId },
                };
            }

            if (advertisement.AuthorId == userId || await this.usersService.IsAdminAsync(userId))
            {
                advertisement.Title = model.Title;
                advertisement.Description = model.Description;
                advertisement.ModifiedOn = DateTime.UtcNow;

                foreach (var imageUrl in model.ImageURLs)
                {
                    await this.imagesService.CreateAsync(imageUrl, advertisementId);
                }

                var editCarRequest = await this.carsService.UpdateAsync(userId, advertisement.CarId, model.Car);

                if (!editCarRequest.Success)
                {
                    return new ResultModel<bool>
                    {
                        Errors = editCarRequest.Errors,
                    };
                }

                this.dbContext.Advertisements.Update(advertisement);
                await this.dbContext.SaveChangesAsync();

                return new ResultModel<bool>
                {
                    Success = true,
                };
            }

            return new ResultModel<bool>
            {
                Errors = new string[] { Errors.NoPermissionToEditAdvertisement },
            };
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
                        Condition = a.Car.Condition.ToString(),
                        Transmission = a.Car.Transmission.ToString(),
                        Mileage = a.Car.Mileage
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
                    CreatedOn = a.CreatedOn.ToString(),
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
                        Email = a.Author.Email,
                        PhoneNumber = a.Author.PhoneNumber,
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
            var carsQuery = this.dbContext.Cars.AsQueryable();

            if (model.MinPrice != 0 && model.MaxPrice != 0)
            {
                carsQuery = carsQuery.Where(c => c.Price > model.MinPrice && c.Price < model.MaxPrice);
            }
            else if (model.MinPrice != 0)
            {
                carsQuery = carsQuery.Where(c => c.Price > model.MinPrice);
            }
            else if (model.MaxPrice != 0)
            {
                carsQuery = carsQuery.Where(c => c.Price < model.MaxPrice);
            }

            if (model.MinYear != 0 && model.MaxYear != 0)
            {
                carsQuery = carsQuery.Where(c => c.Year > model.MinYear && c.Year < model.MaxYear);
            }
            else if (model.MinYear != 0)
            {
                carsQuery = carsQuery.Where(c => c.Year > model.MinYear);
            }
            else if (model.MaxYear != 0)
            {
                carsQuery = carsQuery.Where(c => c.Year < model.MaxYear);
            }

            // cars = model.MinYear != 0 && model.MaxYear != 0 ? cars = cars.Where(a => a.Year > model.MinYear && a.Year < model.MaxYear).ToList()
            //    : model.MinYear != 0 ? cars.Where(a => a.Year > model.MinYear).ToList()
            //    : model.MaxYear != 0 ? cars.Where(a => a.Year < model.MaxYear).ToList() : cars;

            if (model.MinHorsePower != 0 && model.MaxHorsePower != 0)
            {
                carsQuery = carsQuery.Where(c => c.HorsePower > model.MinHorsePower && c.HorsePower < model.MaxHorsePower);
            }
            else if (model.MinHorsePower != 0)
            {
                carsQuery = carsQuery.Where(c => c.HorsePower > model.MinHorsePower);
            }
            else if (model.MaxHorsePower != 0)
            {
                carsQuery = carsQuery.Where(c => c.HorsePower < model.MaxHorsePower);
            }

            if (!string.IsNullOrEmpty(model.FuelType))
            {
                var fuelType = (FuelType)Enum.Parse(typeof(FuelType), model.FuelType);

                carsQuery = carsQuery.Where(c => c.FuelType == fuelType);
            }

            if (!string.IsNullOrEmpty(model.Transmission))
            {
                var transmissionType = (TransmissionType)Enum.Parse(typeof(TransmissionType), model.Transmission);

                carsQuery = carsQuery.Where(c => c.Transmission == transmissionType);
            }

            if (!string.IsNullOrEmpty(model.Color))
            {
                var color = (Color)Enum.Parse(typeof(Color), model.Color);

                carsQuery = carsQuery.Where(c => c.Color == color);
            }

            if (!string.IsNullOrEmpty(model.Location))
            {
                var location = (Location)Enum.Parse(typeof(Location), model.Location);

                carsQuery = carsQuery.Where(c => c.Owner.Location == location);
            }

            if (!string.IsNullOrEmpty(model.Condition))
            {
                var condition = (ConditionType)Enum.Parse(typeof(ConditionType), model.Condition);

                carsQuery = carsQuery.Where(c => c.Condition == condition);
            }

            if (!string.IsNullOrEmpty(model.EuroStandard))
            {
                var euroStandard = (EuroStandard)Enum.Parse(typeof(EuroStandard), model.EuroStandard);

                carsQuery = carsQuery.Where(c => c.EuroStandard == euroStandard);
            }

            if (!string.IsNullOrEmpty(model.DoorsCount))
            {
                var doorsCount = (DoorsCount)Enum.Parse(typeof(DoorsCount), model.DoorsCount);

                carsQuery = carsQuery.Where(c => c.DoorsCount == doorsCount);
            }

            if (!string.IsNullOrEmpty(model.BodyType))
            {
                var bodyType = (BodyType)Enum.Parse(typeof(BodyType), model.BodyType);

                carsQuery = carsQuery.Where(c => c.BodyType == bodyType);
            }

            if (!string.IsNullOrEmpty(model.BrandId))
            {
                carsQuery = carsQuery.Where(c => c.BrandId == model.BrandId);
            }

            if (!string.IsNullOrEmpty(model.ModelId))
            {
                carsQuery = carsQuery.Where(c => c.ModelId == model.ModelId);
            }

            var result = await carsQuery
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
                        Mileage = c.Mileage,
                        Condition = c.Condition.ToString(),
                        Transmission = c.Transmission.ToString(),
                        BodyType = c.BodyType.ToString(),
                        FuelType = c.FuelType.ToString(),
                    }
                })
                .OrderByDescending(c => c.CreatedOn)
                .ToListAsync();

            return result;
        }

        public async Task<ICollection<AdvertisementListingServiceModel>> GetByUserIdAsync(string userId)
        {
            return await this.dbContext
                .Advertisements
                .Where(a => a.AuthorId == userId)
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
                        Condition = a.Car.Condition.ToString(),
                        Transmission = a.Car.Transmission.ToString(),
                        Mileage = a.Car.Mileage
                    }
                })
                .OrderByDescending(a => a.CreatedOn)
                .ToListAsync();
        }

        public async Task<ResultModel<bool>> IncrementViewsAsync(string advertisementId)
        {
            var advertisement = await this.GetByIdAsync(advertisementId);

            if (advertisement == null)
            {
                return new ResultModel<bool>
                {
                    Errors = new string[] { Errors.InvalidAdvertisementId },
                };
            }

            advertisement.Views++;

            this.dbContext.Advertisements.Update(advertisement);
            await this.dbContext.SaveChangesAsync();

            return new ResultModel<bool>
            {
                Success = true,
            };
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
