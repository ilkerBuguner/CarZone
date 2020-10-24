namespace CarZone.Server.Data.Models.Protection
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using CarZone.Server.Data.Common;

    public class CarProtection : BaseDeletableModel<string>
    {
        public CarProtection()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [ForeignKey(nameof(Models.Car))]
        public string CarId { get; set; }

        public virtual Car Car { get; set; }

        [Required]
        [ForeignKey(nameof(Models.Protection.Protection))]
        public string ProtectionId { get; set; }

        public virtual Protection Protection { get; set; }
    }
}
