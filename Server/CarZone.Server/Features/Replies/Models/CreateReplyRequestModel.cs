namespace CarZone.Server.Features.Replies.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CreateReplyRequestModel
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public string AdvertisementId { get; set; }

        [Required]
        public string RootCommentId { get; set; }
    }
}
