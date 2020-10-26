namespace CarZone.Server.Features.BrandModels.Models
{
    using System.ComponentModel.DataAnnotations;

    using static CarZone.Server.Data.Common.Constants.Model;

    public class UpdateBrandModelRequestModel
    {
        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string BrandId { get; set; }
    }
}
