namespace CarZone.Server.Features.CarSafeties.Models
{
    public class UpdateCarSafetyRequestModel
    {
        public string CarId { get; set; }

        public string SafetyId { get; set; }

        public bool IsChecked { get; set; }
    }
}
