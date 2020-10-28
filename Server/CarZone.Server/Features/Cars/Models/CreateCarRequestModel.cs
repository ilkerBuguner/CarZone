namespace CarZone.Server.Features.Cars.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CarZone.Server.Data.Enumerations;
    using CarZone.Server.Data.Models;

    public class CreateCarRequestModel
    {
        [Required]
        public decimal Price { get; set; }

        [Required]
        public string FuelType { get; set; }

        [Required]
        public int HorsePower { get; set; }

        [Required]
        public string Transmission { get; set; }

        [Required]
        public int Year { get; set; }

        public int Mileage { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public string Condition { get; set; }

        [Required]
        public string EuroStandard { get; set; }

        [Required]
        public string DoorsCount { get; set; }

        [Required]
        public string BodyType { get; set; }

        public ICollection<string> Safeties { get; set; }

        public ICollection<string> Exteriors { get; set; }

        public ICollection<string> Protections { get; set; }

        public ICollection<string> Comforts { get; set; }

        [Required]
        public string BrandId { get; set; }

        [Required]
        public string ModelId { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public string AdvertisementId { get; set; }
    }
}
