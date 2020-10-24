using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarZone.Server.Data.Common.Seeding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CarZone.Server.Data.Common
{
    public class DbContextSeeder : ISeeder
    {
        public async Task SeedAsync(CarZoneDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger(typeof(DbContextSeeder));

            var seeders = new List<ISeeder>
                          {
                              new DataSeeder(),
                              new RolesSeeder(),
                              new ComfortSeeder(),
                              new ExteriorSeeder(),
                              new ProtectionSeeder(),
                              new SafetySeeder(),
                              new BrandSeeder(),
                          };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(dbContext, serviceProvider);
                await dbContext.SaveChangesAsync();
                logger.LogInformation($"Seeder {seeder.GetType().Name} done.");
            }
        }
    }
}
