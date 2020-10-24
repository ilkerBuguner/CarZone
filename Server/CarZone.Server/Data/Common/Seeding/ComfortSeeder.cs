using CarZone.Server.Data.Models.Comfort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarZone.Server.Data.Common.Seeding
{
    public class ComfortSeeder : ISeeder
    {
        public async Task SeedAsync(CarZoneDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Comforts.Any())
            {
                return;
            }

            var comforts = new List<string>()
            {
                "Auto Start Stop function",
                "Bluetooth / Handsfree system",
                "DVD, TV",
                "Steptronic, Tiptronic",
                "USB, audio/video, IN/AUX",
                "Adaptive air suspension",
                "Keyless start",
                "Differential Lock",
                "Board computer",
                "Fast / Slow transmission",
                "Light sensor",
                "Electric Mirrors",
                "Electric Windows",
                "Electrical suspension adjustment",
                "Electrical seat adjustment",
                "Electrical power steering",
                "Air conditioner",
                "Climatronic",
                "Multifunction steering wheel",
                "Navigation",
                "Stove",
                "Heated front window",
                "Seat heating",
                "Steering wheel adjustment",
                "Rain sensor",
                "Power steering",
                "Headlamp wash system",
                "Speed control system (autopilot)",
                "Stereo",
                "Filter for hard particles",
                "Refrigerator",
            };

            foreach (var comfortName in comforts)
            {
                await dbContext.Comforts.AddAsync(new Comfort { Name = comfortName });
            }

        }
    }
}
