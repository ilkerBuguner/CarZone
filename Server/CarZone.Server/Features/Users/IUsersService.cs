using System.Threading.Tasks;

namespace CarZone.Server.Features.Users
{
    public interface IUsersService
    {
        Task<bool> IsAdminAsync(string userId);
    }
}
