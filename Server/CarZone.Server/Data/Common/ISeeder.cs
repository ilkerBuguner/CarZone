using System;
using System.Threading.Tasks;

namespace CarZone.Server.Data.Common
{
    public interface ISeeder
    {
        Task SeedAsync(CarZoneDbContext dbContext, IServiceProvider serviceProvider);
    }
}
