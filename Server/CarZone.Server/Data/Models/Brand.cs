namespace CarZone.Server.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CarZone.Server.Data.Common;

    using static CarZone.Server.Data.Common.Constants.Brand;

    public class Brand : BaseDeletableModel<string>
    {
        public Brand()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Models = new HashSet<Model>();
            this.Cars = new HashSet<Car>();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public ICollection<Model> Models { get; set; }

        public ICollection<Car> Cars { get; set; }
    }
}
