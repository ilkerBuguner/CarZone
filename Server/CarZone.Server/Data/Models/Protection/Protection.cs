using CarZone.Server.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarZone.Server.Data.Models.Protection
{
    public class Protection : BaseDeletableModel<string>
    {
        public Protection()
        {
            this.Id = Guid.NewGuid().ToString();

            this.CarProtections = new HashSet<CarProtection>();
        }

        public string Name { get; set; }

        public ICollection<CarProtection> CarProtections { get; set; }
    }
}
