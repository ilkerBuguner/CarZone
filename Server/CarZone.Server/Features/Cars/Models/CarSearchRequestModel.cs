namespace CarZone.Server.Features.Cars.Models
{
    public class CarSearchRequestModel
    {
        public decimal MinPrice { get; set; }

        public decimal MaxPrice { get; set; }

        public int MinYear { get; set; }

        public int MaxYear { get; set; }

        public int MinHorsePower { get; set; }

        public int MaxHorsePower { get; set; }

        public string FuelType { get; set; }

        public string Transmission { get; set; }

        public string Color { get; set; }

        public string Condition { get; set; }

        public string EuroStandard { get; set; }

        public string DoorsCount { get; set; }

        public string BodyType { get; set; }

        public string BrandName { get; set; }

        public string ModelName { get; set; }
    }
}
