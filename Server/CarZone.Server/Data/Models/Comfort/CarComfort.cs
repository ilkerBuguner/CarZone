using CarZone.Server.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarZone.Server.Data.Models.Comfort
{
    public class CarComfort : BaseDeletableModel<string>
    {
        public CarComfort()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [ForeignKey(nameof(Models.Car))]
        public string CarId { get; set; }

        public virtual Car Car { get; set; }

        [Required]
        [ForeignKey(nameof(Models.Comfort.Comfort))]
        public string ComfortId { get; set; }

        public virtual Comfort Comfort { get; set; }
    }
}
