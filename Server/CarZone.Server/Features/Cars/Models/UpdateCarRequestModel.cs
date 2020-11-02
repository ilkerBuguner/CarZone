namespace CarZone.Server.Features.Cars.Models
{
    public class UpdateCarRequestModel
    {
        public decimal Price { get; set; }

        public string FuelType { get; set; }

        public int HorsePower { get; set; }

        public string Transmission { get; set; }

        public int Year { get; set; }

        public int Mileage { get; set; }

        public string Color { get; set; }

        public string Condition { get; set; }

        public string EuroStandard { get; set; }

        public string DoorsCount { get; set; }

        public string BodyType { get; set; }

        public string BrandId { get; set; }

        public string ModelId { get; set; }
    }
}
