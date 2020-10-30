namespace CarZone.Server.Features.Comments.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CreateCommentRequestModel
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public string AdvertisementId { get; set; }
    }
}
