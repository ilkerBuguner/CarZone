using CarZone.Server.Data.Models.Protection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarZone.Server.Data.Common.Seeding
{
    public class ProtectionSeeder : ISeeder
    {
        public async Task SeedAsync(CarZoneDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Exteriors.Any())
            {
                return;
            }

            var protections = new List<string>()
            {
                "OFFROAD package",
                "Alarm",
                "Armored",
                "Immobilizer",
                "Casco",
                "Winch",
                "Reinforced glass",
                "Central locking",
            };

            foreach (var protectionName in protections)
            {
                await dbContext.Protections.AddAsync(new Protection { Name = protectionName });
            }
        }
    }
}
