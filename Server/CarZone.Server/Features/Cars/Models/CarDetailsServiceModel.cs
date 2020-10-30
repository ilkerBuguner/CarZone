namespace CarZone.Server.Features.Cars.Models
{
    using System.Collections.Generic;

    using CarZone.Server.Features.BrandModels.Models;
    using CarZone.Server.Features.Brands.Models;

    public class CarDetailsServiceModel
    {
        public string Id { get; set; }

        public decimal Price { get; set; }

        public string FuelType { get; set; }

        public int HorsePower { get; set; }

        public string Transmission { get; set; }

        public int Year { get; set; }

        public int Mileage { get; set; }

        public string Color { get; set; }

        public string Condition { get; set; }

        public string EuroStandard { get; set; }

        public string DoorsCount { get; set; }

        public string BodyType { get; set; }

        public ICollection<string> Safeties { get; set; }

        public ICollection<string> Exteriors { get; set; }

        public ICollection<string> Protections { get; set; }

        public ICollection<string> Comforts { get; set; }

        public BrandInfoServiceModel Brand { get; set; }

        public BrandModelInfoServiceModel Model { get; set; }
    }
}
