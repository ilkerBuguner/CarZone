namespace CarZone.Server.Data.Models.Comfort
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CarZone.Server.Data.Common;

    using static CarZone.Server.Data.Common.Constants.Comfort;

    public class Comfort : BaseDeletableModel<string>
    {
        public Comfort()
        {
            this.Id = Guid.NewGuid().ToString();

            this.CarComforts = new HashSet<CarComfort>();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public ICollection<CarComfort> CarComforts { get; set; }
    }
}
