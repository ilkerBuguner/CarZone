namespace CarZone.Server.Features.Advertisements.Models
{
    using System.Collections.Generic;

    using CarZone.Server.Features.Cars.Models;
    using CarZone.Server.Features.Images.Models;
    using CarZone.Server.Features.Users.Models;

    public class AdvertisementDetailsServiceModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Views { get; set; }

        public string Location { get; set; }

        public string CreatedOn { get; set; }

        public CarDetailsServiceModel Car { get; set; }

        public UserInfoServiceModel Author { get; set; }

        public ICollection<ImageInfoServiceModel> Images { get; set; }
    }
}
