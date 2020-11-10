namespace CarZone.Server.Features.Identity.Models
{
    public class AuthResponseModel
    {
        public string Token { get; set; }

        public bool IsAdmin { get; set; }

        public UserDetailsServiceModel User { get; set; }
    }
}
