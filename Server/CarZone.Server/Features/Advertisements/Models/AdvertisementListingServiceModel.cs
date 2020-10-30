namespace CarZone.Server.Features.Advertisements.Models
{
    using System;
    using System.Collections.Generic;

    using CarZone.Server.Features.Cars.Models;
    using CarZone.Server.Features.Users.Models;

    public class AdvertisementListingServiceModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public int Views { get; set; }

        public string CreatedOn { get; set; }

        public string Location { get; set; }

        public CarInfoServiceModel Car { get; set; }

        public UserInfoServiceModel Author { get; set; }

        public string ImageUrl { get; set; }
    }
}
