namespace CarZone.Server.Data.Models.Exterior
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CarZone.Server.Data.Common;

    using static CarZone.Server.Data.Common.Constants.Exterior;

    public class Exterior : BaseDeletableModel<string>
    {
        public Exterior()
        {
            this.Id = Guid.NewGuid().ToString();

            this.CarExteriors = new HashSet<CarExterior>();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public ICollection<CarExterior> CarExteriors { get; set; }
    }
}
