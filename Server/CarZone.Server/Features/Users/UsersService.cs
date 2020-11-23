namespace CarZone.Server.Features.Users
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CarZone.Server.Data;
    using CarZone.Server.Data.Enumerations;
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

        public async Task<ResultModel<bool>> UpdateAsync(string currentLoggedInUserId, string targetUserId, UpdateUserRequestModel model)
        {
            var user = await this.GetByIdAsync(targetUserId);

            if (user == null)
            {
                return new ResultModel<bool>
                {
                    Errors = new string[] { Errors.InvalidUserId },
                };
            }

            if (user.Id == currentLoggedInUserId || await this.IsAdminAsync(currentLoggedInUserId))
            {
                user.UserName = model.Username;
                user.FullName = model.FullName;
                user.PhoneNumber = model.PhoneNumber;
                user.Location = (Location)Enum.Parse(typeof(Location), model.Location);
                user.Gender = (Gender)Enum.Parse(typeof(Gender), model.Gender);

                this.dbContext.Users.Update(user);
                await this.dbContext.SaveChangesAsync();

                return new ResultModel<bool>
                {
                    Success = true,
                };
            }

            return new ResultModel<bool>
            {
                Errors = new string[] { Errors.NoPermissionToEditUser },
            };
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
                        ShortDescription = a.Description.Substring(0, 70) + "...",
                        CommentsCount = a.Comments.Count,
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

        public async Task SetUserPhoneNumberIfNull(string userId, string phoneNumber)
        {
            var user = await this.GetByIdAsync(userId);

            if (string.IsNullOrEmpty(user.PhoneNumber))
            {
                user.PhoneNumber = phoneNumber;
                this.dbContext.Users.Update(user);
                await this.dbContext.SaveChangesAsync();
            }
        }

        public async Task SetUserLocationIfNull(string userId, string location)
        {
            var user = await this.GetByIdAsync(userId);

            if (user.Location == null)
            {
                user.Location = (Location)Enum.Parse(typeof(Location), location);
                this.dbContext.Users.Update(user);
                await this.dbContext.SaveChangesAsync();
            }
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
