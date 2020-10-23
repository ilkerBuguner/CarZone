using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using CarZone.Server.Data.Common;

using static CarZone.Server.Data.Common.Constants.Reply;

namespace CarZone.Server.Data.Models
{
    public class Reply : BaseDeletableModel<string>
    {
        public Reply()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        [ForeignKey(nameof(User))]
        [Required]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        [ForeignKey(nameof(Models.Advertisement))]
        [Required]
        public string AdvertisementId { get; set; }

        public virtual Advertisement Advertisement { get; set; }

        [ForeignKey(nameof(Models.Comment))]
        [Required]
        public string RootCommentId { get; set; }

        public virtual Comment RootComment { get; set; }
    }
}
