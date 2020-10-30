namespace CarZone.Server.Features.Users
{
    using System.Linq;
    using System.Threading.Tasks;

    using CarZone.Server.Data;
    using CarZone.Server.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using static CarZone.Server.Data.Common.Constants.Seeding;

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

        private async Task<User> GetByIdAsync(string id)
        {
            return await this.dbContext
                .Users
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
