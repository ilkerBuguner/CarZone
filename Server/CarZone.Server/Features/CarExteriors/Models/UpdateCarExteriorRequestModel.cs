namespace CarZone.Server.Features.CarExteriors.Models
{
    public class UpdateCarExteriorRequestModel
    {
        public string CarId { get; set; }

        public string ExteriorId { get; set; }

        public bool IsChecked { get; set; }
    }
}
