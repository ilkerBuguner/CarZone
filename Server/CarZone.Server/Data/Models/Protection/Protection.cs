using CarZone.Server.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static CarZone.Server.Data.Common.Constants.Protection;

namespace CarZone.Server.Data.Models.Protection
{
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
