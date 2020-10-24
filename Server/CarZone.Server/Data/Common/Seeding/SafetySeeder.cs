namespace CarZone.Server.Data.Common.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CarZone.Server.Data.Models.Safety;

    public class SafetySeeder : ISeeder
    {
        public async Task SeedAsync(CarZoneDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Safeties.Any())
            {
                return;
            }

            var safeties = new List<string>()
            {
                "GPS tracking system",
                "Automatic stability control",
                "Adaptive headlights",
                "Anti-lock braking system (ABS)",
                "Airbags - Rear",
                "Airbags - Front",
                "Airbags - Side",
                "Electric brake force distribution",
                "Electronic stabilization program (ESP)",
                "Tire pressure control",
                "Parktronic",
                "ISOFIX system",
                "Dynamic stability system",
                "Slip protection system",
                "Brake drying system",
                "Distance control system",
                "Descent control system",
                "Brake assist system",
            };

            foreach (var safetyName in safeties)
            {
                await dbContext.Safeties.AddAsync(new Safety { Name = safetyName });
            }
        }
    }
}
