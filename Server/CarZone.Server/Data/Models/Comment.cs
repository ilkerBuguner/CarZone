using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using CarZone.Server.Data.Common;

using static CarZone.Server.Data.Common.Constants.Comment;

namespace CarZone.Server.Data.Models
{
    public class Comment : BaseDeletableModel<string>
    {
        public Comment()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Replies = new HashSet<Reply>();
        }

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        [ForeignKey(nameof(Models.User))]
        [Required]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        [ForeignKey(nameof(Models.Advertisement))]
        [Required]
        public string AdvertisementId { get; set; }

        public virtual Advertisement Advertisement { get; set; }

        public virtual ICollection<Reply> Replies { get; set; }
    }
}
