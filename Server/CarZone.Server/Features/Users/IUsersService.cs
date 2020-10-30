namespace CarZone.Server.Features.Users
{
    using System.Threading.Tasks;

    using CarZone.Server.Features.Common.Models;
    using CarZone.Server.Features.Users.Models;

    public interface IUsersService
    {
        Task<bool> IsAdminAsync(string userId);

        Task<ResultModel<bool>> UpdateAsync(string currentLoggedInUserId, string userId, UpdateUserRequestModel model);

        Task<ResultModel<UserProfileDetailsServiceModel>> DetailsAsync(string id);
    }
}
