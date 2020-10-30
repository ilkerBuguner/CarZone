namespace CarZone.Server.Features.Users.Models
{
    public class UpdateUserRequestModel
    {
        public string Username { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string Location { get; set; }

        public string Gender { get; set; }
    }
}
