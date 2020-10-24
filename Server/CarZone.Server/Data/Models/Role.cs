using CarZone.Server.Data.Common;
using Microsoft.AspNetCore.Identity;
using System;

namespace CarZone.Server.Data.Models
{
    public class Role : IdentityRole, IAuditInfo, IDeletableEntity
    {
        public Role()
            : this(null)
        {
        }

        public Role(string name)
            : base(name)
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
            this.IsDeleted = false;
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
