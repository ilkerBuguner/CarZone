using CarZone.Server.Data;
using CarZone.Server.Data.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CarZone.Server.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();

            var dbContext = services.ServiceProvider.GetService<CarZoneDbContext>();

            dbContext.Database.Migrate();
            new DbContextSeeder().SeedAsync(dbContext, services.ServiceProvider).GetAwaiter().GetResult();
        }
    }
}
