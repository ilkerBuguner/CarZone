namespace CarZone.Server.Data.Common
{
    using System;
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task SeedAsync(CarZoneDbContext dbContext, IServiceProvider serviceProvider);
    }
}
