namespace CarZone.Server.Features.Users.Models
{
    using System.Collections.Generic;

    using CarZone.Server.Features.Advertisements.Models;

    public class UserProfileDetailsServiceModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string CreatedOn { get; set; }

        public string Location { get; set; }

        public string Gender { get; set; }

        public string ProfilePictureUrl { get; set; }

        public ICollection<AdvertisementListingServiceModel> Advertisements { get; set; }
    }
}
