﻿namespace CarZone.Server.Features.Users
{
    using System.Linq;
    using System.Threading.Tasks;

    using CarZone.Server.Data;
    using CarZone.Server.Data.Models;
    using CarZone.Server.Features.Advertisements.Models;
    using CarZone.Server.Features.Common.Models;
    using CarZone.Server.Features.Users.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using static CarZone.Server.Data.Common.Constants.Seeding;
    using static CarZone.Server.Features.Common.Constants;

    public class UsersService : IUsersService
    {
        private readonly CarZoneDbContext dbContext;
        private readonly UserManager<User> userManager;

        public UsersService(
            CarZoneDbContext dbContext,
            UserManager<User> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task<bool> IsAdminAsync(string userId)
        {
            var user = await this.GetByIdAsync(userId);

            return await this.userManager.IsInRoleAsync(user, AdministratorRoleName);
        }

        public async Task<ResultModel<UserProfileDetailsServiceModel>> DetailsAsync(string id)
        {
            var user = await this.dbContext
                .Users
                .Where(u => u.Id == id)
                .Select(u => new UserProfileDetailsServiceModel
                {
                    Id = u.Id,
                    Username = u.UserName,
                    FullName = u.FullName,
                    PhoneNumber = u.PhoneNumber,
                    Email = u.Email,
                    CreatedOn = u.CreatedOn.ToString(),
                    ProfilePictureUrl = u.ProfilePictureUrl,
                    Gender = u.Gender != null ? u.Gender.ToString() : null,
                    Location = u.Location != null ? u.Location.ToString() : null,
                    Advertisements = u.Advertisements.Select(a => new AdvertisementListingServiceModel
                    {
                        Id = a.Id,
                        Title = a.Title,
                        Views = a.Views,
                        ImageUrl = a.Images.Select(x => x.Url).FirstOrDefault(),
                        CreatedOn = a.CreatedOn.ToString(),
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return new ResultModel<UserProfileDetailsServiceModel>
                {
                    Errors = new string[] { Errors.InvalidUserId },
                };
            }

            return new ResultModel<UserProfileDetailsServiceModel>
            {
                Success = true,
                Result = user,
            };
        }

        private async Task<User> GetByIdAsync(string id)
        {
            return await this.dbContext
                .Users
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
