using CarZone.Server.Data.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarZone.Server.Data.Models.Exterior
{
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
