namespace CarZone.Server.Features.Identity
{
    using System.Threading.Tasks;

    using CarZone.Server.Features.Common.Models;
    using CarZone.Server.Features.Identity.Models;

    public interface IIdentityService
    {
        string GenerateJwtToken(string userId, string username, string secret);

        Task<ResultModel<AuthResponseModel>> RegisterAsync(string fullName, string userName, string email, string password, string secret);

        Task<ResultModel<AuthResponseModel>> LoginAsync(string email, string password, string secret);
    }
}
