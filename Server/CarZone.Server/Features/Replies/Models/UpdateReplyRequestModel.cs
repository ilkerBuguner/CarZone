namespace CarZone.Server.Features.Replies.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UpdateReplyRequestModel
    {
        [Required]
        public string Content { get; set; }
    }
}
