namespace CarZone.Server.Features.Advertisements.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CarZone.Server.Features.Cars.Models;

    using static CarZone.Server.Data.Common.Constants.Advertisement;

    public class CreateAdvertisementRequestModel
    {
        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(DescriptionMinLength)]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Location { get; set; }

        public string AuthorId { get; set; }

        public ICollection<string> ImageURLs { get; set; }

        public CreateCarRequestModel Car { get; set; }
    }
}
