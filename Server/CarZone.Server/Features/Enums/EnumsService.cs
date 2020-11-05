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
            var enums = new EnumsServiceModel
            {
                BodyTypes = Enum.GetValues(typeof(BodyType)).Cast<string>(),
                Colors = Enum.GetValues(typeof(Color)).Cast<string>(),
                ConditionTypes = Enum.GetValues(typeof(Color)).Cast<string>(),
                DoorsCounts = Enum.GetValues(typeof(DoorsCount)).Cast<string>(),
                EuroStandards = Enum.GetValues(typeof(EuroStandard)).Cast<string>(),
                FuelTypes = Enum.GetValues(typeof(FuelType)).Cast<string>(),
                Genders = Enum.GetValues(typeof(Gender)).Cast<string>(),
                Locations = Enum.GetValues(typeof(Location)).Cast<string>(),
                TransmissionTypes = Enum.GetValues(typeof(TransmissionType)).Cast<string>(),
            };

            return enums;
        }
    }
}
