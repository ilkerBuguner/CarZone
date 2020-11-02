namespace CarZone.Server.Features.Advertisements.Models
{
    using System.ComponentModel.DataAnnotations;

    using CarZone.Server.Features.Cars.Models;

    using static CarZone.Server.Data.Common.Constants.Advertisement;

    public class UpdateAdvertisementRequestModel
    {
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Title { get; set; }

        [MinLength(DescriptionMinLength)]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public UpdateCarRequestModel Car { get; set; }
    }
}
