namespace CarZone.Server.Data.Common.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CarZone.Server.Data.Models;

    public class BrandSeeder : ISeeder
    {
        public async Task SeedAsync(CarZoneDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Brands.Any())
            {
                return;
            }

            var brands = new List<string>()
            {
                // A
                "AC",
                "Abarth",
                "Acura",
                "Aixam",
                "Alfa Romeo",
                "Alpina",
                "Aro",
                "Asia",
                "Aston Martin",
                "Audi",
                "Austin",

                // B
                "BMW",
                "Bentley",
                "Berliner",
                "Bertone",
                "Borgward",
                "Brilliance",
                "Bugatti",
                "Buick",

                // C
                "Cadillac",
                "Chevrolet",
                "Chrysler",
                "Citroen",
                "Corvette",
                "Cupra",

                // D
                "DS",
                "Dacia",
                "Daewoo",
                "Daihatsu",
                "Daimler",
                "Datsun",
                "Dkw",
                "Dodge",
                "Dr",

                // E
                "Eagle",

                // F
                "FSO",
                "Ferrari",
                "Fiat",
                "Ford",

                // G
                "GOUPIL",
                "Gaz",
                "Geo",
                "Gmc",
                "Great Wall",

                // H
                "Haval",
                "Hinkel",
                "Hillman",
                "Honda",
                "Hyummer",
                "Hyundai",

                // I
                "Ifa",
                "Infiniti",
                "Innocenti",
                "Isuzu",
                "Iveco",

                // J
                "JAS",
                "Jaguar",
                "Jeep",
                "Jpx",

                // K
                "Kia",

                // L
                "Lada",
                "Laforza",
                "Lamborghini",
                "Lancia",
                "Land Rover",
                "Landwind",
                "Lexus",
                "Lifan",
                "Lincoln",
                "Lotus",

                // M
                "MG",
                "Mahindra",
                "Maserati",
                "Matra",
                "Maybach",
                "Mazda",
                "McLaren",
                "Mercedes-Benz",
                "Mercury",
                "Mg",
                "Microcar",
                "Mini",
                "Mitsubishi",
                "Morgan",
                "Moskvich",

                // N
                "Nissan",

                // O
                "Oldmobile",
                "Opel",

                // P
                "Perodua",
                "Peugeot",
                "Pgo",
                "Plymouth",
                "Polonez",
                "Pontiac",
                "Porsche",
                "Proton",

                // R
                "Renault",
                "Rolls-Royce",
                "Rover",

                // S
                "SECMA",
                "SH auto",
                "Saab",
                "Samand",
                "Santana",
                "Saturn",
                "Scion",
                "Seat",
                "Shatenet",
                "Shuanghuan",
                "Simca",
                "Skoda",
                "Smart",
                "Ssang yong",
                "Subaru",
                "Suzuki",

                // T
                "Talbot",
                "Tata",
                "Tavria",
                "Tazzari",
                "Tempo",
                "Terberg",
                "Tesla",
                "Tofas",
                "Toyota",
                "Trabant",
                "Triumph",

                // U
                "Uaz",

                // V
                "VROMOS",
                "VW",
                "Volga",
                "Volvo",

                // W
                "Warszawa",
                "Wartburg",
                "Wiesmann",

                // X
                "Xinkai",
                "Xinshun",

                // Z
                "Zastava",
                "Zaz",

                //
                "Other",
            };

            foreach (var brandName in brands)
            {
                await dbContext.Brands.AddAsync(new Brand { Name = brandName });
            }
        }
    }
}
