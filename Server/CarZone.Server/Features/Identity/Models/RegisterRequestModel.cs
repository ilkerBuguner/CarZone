namespace CarZone.Server.Features.Identity.Models
{
    using System.ComponentModel.DataAnnotations;

    using static CarZone.Server.Data.Common.Constants.User;

    public class RegisterRequestModel
    {
        [Required]
        [MinLength(UserNameMinLength)]
        [MaxLength(UserNameMaxLength)]
        public string UserName { get; set; }

        [Required]
        [MinLength(UserNameMinLength)]
        [MaxLength(UserNameMaxLength)]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
