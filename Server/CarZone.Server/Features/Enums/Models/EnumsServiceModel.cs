namespace CarZone.Server.Features.Enums.Models
{
    using System.Collections.Generic;

    public class EnumsServiceModel
    {
        public IEnumerable<string> BodyTypes { get; set; }

        public IEnumerable<string> Colors { get; set; }

        public IEnumerable<string> ConditionTypes { get; set; }

        public IEnumerable<string> DoorsCounts { get; set; }

        public IEnumerable<string> EuroStandards { get; set; }

        public IEnumerable<string> FuelTypes { get; set; }

        public IEnumerable<string> Genders { get; set; }

        public IEnumerable<string> Locations { get; set; }

        public IEnumerable<string> TransmissionTypes { get; set; }
    }
}
