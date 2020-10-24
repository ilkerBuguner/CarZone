namespace CarZone.Server.Data.Models.Safety
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CarZone.Server.Data.Common;

    using static CarZone.Server.Data.Common.Constants.Safety;

    public class Safety : BaseDeletableModel<string>
    {
        public Safety()
        {
            this.Id = Guid.NewGuid().ToString();

            this.CarSafeties = new HashSet<CarSafety>();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public ICollection<CarSafety> CarSafeties { get; set; }
    }
}
