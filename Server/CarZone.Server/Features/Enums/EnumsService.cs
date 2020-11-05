namespace CarZone.Server.Features.Enums
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CarZone.Server.Data.Enumerations;
    using CarZone.Server.Features.Enums.Models;

    public class EnumsService : IEnumsService
    {
        public EnumsServiceModel GetAll()
        {
            var enums = new EnumsServiceModel();

            var bodyTypes = new List<string>();
            var colors = new List<string>();
            var conditionTypes = new List<string>();
            var doorsCounts = new List<string>();
            var euroStandards = new List<string>();
            var fuelTypes = new List<string>();
            var genders = new List<string>();
            var locations = new List<string>();
            var transmissionTypes = new List<string>();

            foreach (var bodyType in Enum.GetValues(typeof(BodyType)))
            {
                bodyTypes.Add(bodyType.ToString());
            }

            foreach (var color in Enum.GetValues(typeof(Color)))
            {
                colors.Add(color.ToString());
            }

            foreach (var conditionType in Enum.GetValues(typeof(ConditionType)))
            {
                conditionTypes.Add(conditionType.ToString());
            }

            foreach (var doorsCount in Enum.GetValues(typeof(DoorsCount)))
            {
                doorsCounts.Add(doorsCount.ToString());
            }

            foreach (var euroStandard in Enum.GetValues(typeof(EuroStandard)))
            {
                euroStandards.Add(euroStandard.ToString());
            }

            foreach (var fuelType in Enum.GetValues(typeof(FuelType)))
            {
                fuelTypes.Add(fuelType.ToString());
            }

            foreach (var gender in Enum.GetValues(typeof(Gender)))
            {
                genders.Add(gender.ToString());
            }

            foreach (var location in Enum.GetValues(typeof(Location)))
            {
                locations.Add(location.ToString());
            }

            foreach (var transmissionType in Enum.GetValues(typeof(TransmissionType)))
            {
                transmissionTypes.Add(transmissionType.ToString());
            }

            enums.BodyTypes = bodyTypes;
            enums.Colors = colors;
            enums.ConditionTypes = conditionTypes;
            enums.DoorsCounts = doorsCounts;
            enums.EuroStandards = euroStandards;
            enums.FuelTypes = fuelTypes;
            enums.Genders = genders;
            enums.Locations = locations;
            enums.TransmissionTypes = transmissionTypes;

            return enums;
        }
    }
}
