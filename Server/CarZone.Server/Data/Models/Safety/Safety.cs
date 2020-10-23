using CarZone.Server.Data.Common;
using System;
using System.Collections.Generic;

namespace CarZone.Server.Data.Models.Safety
{
    public class Safety : BaseDeletableModel<string>
    {
        public Safety()
        {
            this.Id = Guid.NewGuid().ToString();

            this.CarSafeties = new HashSet<CarSafety>();
        }

        public string Name { get; set; }

        public ICollection<CarSafety> CarSafeties { get; set; }
    }
}
