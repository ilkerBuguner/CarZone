namespace CarZone.Server.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using CarZone.Server.Data.Common;
    using CarZone.Server.Data.Enumerations;
    using CarZone.Server.Data.Models.Comfort;
    using CarZone.Server.Data.Models.Exterior;
    using CarZone.Server.Data.Models.Protection;
    using CarZone.Server.Data.Models.Safety;

    using static CarZone.Server.Data.Common.Constants.Car;

    public class Car : BaseDeletableModel<string>
    {
        public Car()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Safeties = new HashSet<CarSafety>();
            this.Exteriors = new HashSet<CarExterior>();
            this.Protections = new HashSet<CarProtection>();
            this.Comforts = new HashSet<CarComfort>();
        }

        [Required]
        [Column(TypeName = "decimal(15,2)")]
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

        public ICollection<CarSafety> Safeties { get; set; }

        public ICollection<CarExterior> Exteriors { get; set; }

        public ICollection<CarProtection> Protections { get; set; }

        public ICollection<CarComfort> Comforts { get; set; }

        [ForeignKey(nameof(Models.Brand))]
        [Required]
        public string BrandId { get; set; }

        public virtual Brand Brand { get; set; }

        [ForeignKey(nameof(Models.Model))]
        [Required]
        public string ModelId { get; set; }

        public virtual Model Model { get; set; }

        [ForeignKey(nameof(Models.User))]
        [Required]
        public string OwnerId { get; set; }

        public virtual User Owner { get; set; }

        public string AdvertisementId { get; set; }

        public virtual Advertisement Advertisement { get; set; }
    }
}
