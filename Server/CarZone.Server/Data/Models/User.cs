namespace CarZone.Server.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CarZone.Server.Data.Common;
    using CarZone.Server.Data.Enumerations;
    using Microsoft.AspNetCore.Identity;

    using static CarZone.Server.Data.Common.Constants.User;

    public class User : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Advertisements = new HashSet<Advertisement>();
            this.Comments = new HashSet<Comment>();
            this.Replies = new HashSet<Reply>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        [MaxLength(FullNameMaxLength)]
        public string FullName { get; set; }

        public Location? Location { get; set; }

        public Gender? Gender { get; set; }

        public string ProfilePictureUrl { get; set; }

        public ICollection<Advertisement> Advertisements { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Reply> Replies { get; set; }
    }
}
