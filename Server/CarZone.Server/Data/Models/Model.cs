using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using CarZone.Server.Data.Common;

using static CarZone.Server.Data.Common.Constants.Model;

namespace CarZone.Server.Data.Models
{
    public class Model : BaseDeletableModel<string>
    {
        public Model()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Cars = new HashSet<Car>();
        }

        [Required]
        [MinLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [ForeignKey(nameof(Models.Brand))]
        public string BrandId { get; set; }

        public virtual Brand Brand { get; set; }

        public ICollection<Car> Cars { get; set; }
    }
}
