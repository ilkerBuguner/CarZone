namespace CarZone.Server.Features.CarComforts.Models
{
    public class UpdateCarComfortRequestModel
    {
        public string CarId { get; set; }

        public string ComfortId { get; set; }

        public bool IsChecked { get; set; }
    }
}
