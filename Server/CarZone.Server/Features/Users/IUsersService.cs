namespace CarZone.Server.Features.Users
{
    using System.Threading.Tasks;

    public interface IUsersService
    {
        Task<bool> IsAdminAsync(string userId);
    }
}
