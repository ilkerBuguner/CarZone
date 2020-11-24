namespace CarZone.Server.Features.Users.Models
{
    using System.ComponentModel.DataAnnotations;

    using static CarZone.Server.Data.Common.Constants.User;

    public class UpdateUserRequestModel
    {
        [MinLength(FullNameMinLength)]
        [MaxLength(FullNameMaxLength)]
        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Location { get; set; }

        public string Gender { get; set; }

        public string ProfilePictureUrl { get; set; }
    }
}
