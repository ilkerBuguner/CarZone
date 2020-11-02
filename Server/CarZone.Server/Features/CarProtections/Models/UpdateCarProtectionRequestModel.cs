namespace CarZone.Server.Features.CarProtections.Models
{
    public class UpdateCarProtectionRequestModel
    {
        public string CarId { get; set; }

        public string ProtectionId { get; set; }

        public bool IsChecked { get; set; }
    }
}
