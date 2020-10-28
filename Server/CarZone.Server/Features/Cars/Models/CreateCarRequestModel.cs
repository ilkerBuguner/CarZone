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
        public FuelType FuelType { get; set; }

        [Required]
        public int HorsePower { get; set; }

        [Required]
        public TransmissionType Transmission { get; set; }

        [Required]
        public int Year { get; set; }

        public int Mileage { get; set; }

        public Color Color { get; set; }

        [Required]
        public ConditionType Condition { get; set; }

        public EuroStandard EuroStandard { get; set; }

        public DoorsCount DoorsCount { get; set; }

        [Required]
        public BodyType BodyType { get; set; }

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
