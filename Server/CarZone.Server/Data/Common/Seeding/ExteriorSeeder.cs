namespace CarZone.Server.Data.Common.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CarZone.Server.Data.Models.Exterior;

    public class ExteriorSeeder : ISeeder
    {
        public async Task SeedAsync(CarZoneDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Exteriors.Any())
            {
                return;
            }

            var exteriors = new List<string>()
            {
                "LED headlights",
                "Summer wheels",
                "Xenon lights",
                "Metallic",
                "Heated wipers",
                "Panoramic sunroof",
                "Roof railing",
                "Rollbars",
                "Spoilers",
                "Towbar",
                "Halogen headlights",
                "Shibedah",
            };

            foreach (var exteriorName in exteriors)
            {
                await dbContext.Exteriors.AddAsync(new Exterior { Name = exteriorName });
            }
        }
    }
}
