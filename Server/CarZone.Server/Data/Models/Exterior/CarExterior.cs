namespace CarZone.Server.Data.Models.Exterior
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using CarZone.Server.Data.Common;

    public class CarExterior : BaseDeletableModel<string>
    {
        public CarExterior()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [ForeignKey(nameof(Models.Car))]
        public string CarId { get; set; }

        public virtual Car Car { get; set; }

        [Required]
        [ForeignKey(nameof(Models.Exterior.Exterior))]
        public string ExteriorId { get; set; }

        public virtual Exterior Exterior { get; set; }
    }
}
