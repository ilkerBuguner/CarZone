namespace CarZone.Server.Features.Cars.Models
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CarZone.Server.Features.CarComforts.Models;
    using CarZone.Server.Features.CarExteriors.Models;
    using CarZone.Server.Features.CarProtections.Models;
    using CarZone.Server.Features.CarSafeties.Models;

    public class UpdateCarRequestModel
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

        [Required]
        public string BrandId { get; set; }

        [Required]
        public string ModelId { get; set; }

        public ICollection<UpdateCarComfortRequestModel> CarComforts { get; set; }

        public ICollection<UpdateCarExteriorRequestModel> CarExteriors { get; set; }

        public ICollection<UpdateCarProtectionRequestModel> CarProtections { get; set; }

        public ICollection<UpdateCarSafetyRequestModel> CarSafeties { get; set; }
    }
}
