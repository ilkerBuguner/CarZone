namespace CarZone.Server.Data.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using CarZone.Server.Data.Common;

    using static CarZone.Server.Data.Common.Constants.Advertisement;

    public class Advertisement : BaseDeletableModel<string>
    {
        public Advertisement()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Images = new HashSet<Image>();
            this.Comments = new HashSet<Comment>();
            this.Replies = new HashSet<Reply>();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public int Views { get; set; }

        [Required]
        public string CarId { get; set; }

        public Car Car { get; set; }

        [ForeignKey(nameof(Models.User))]
        [Required]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public ICollection<Image> Images { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Reply> Replies { get; set; }
    }
}
