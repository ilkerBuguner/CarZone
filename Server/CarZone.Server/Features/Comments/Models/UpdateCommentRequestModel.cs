namespace CarZone.Server.Features.Comments.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UpdateCommentRequestModel
    {
        [Required]
        public string Content { get; set; }
    }
}
