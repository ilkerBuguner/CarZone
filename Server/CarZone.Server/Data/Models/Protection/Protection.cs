namespace CarZone.Server.Data.Models.Protection
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CarZone.Server.Data.Common;

    using static CarZone.Server.Data.Common.Constants.Protection;

    public class Protection : BaseDeletableModel<string>
    {
        public Protection()
        {
            this.Id = Guid.NewGuid().ToString();

            this.CarProtections = new HashSet<CarProtection>();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public ICollection<CarProtection> CarProtections { get; set; }
    }
}
