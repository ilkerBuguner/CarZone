namespace CarZone.Server.Data.Models.Safety
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using CarZone.Server.Data.Common;

    public class CarSafety : BaseDeletableModel<string>
    {
        public CarSafety()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [ForeignKey(nameof(Models.Car))]
        public string CarId { get; set; }

        public virtual Car Car { get; set; }

        [Required]
        [ForeignKey(nameof(Models.Safety.Safety))]
        public string SafetyId { get; set; }

        public virtual Safety Safety { get; set; }
    }
}
