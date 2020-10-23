using CarZone.Server.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarZone.Server.Data.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> entity)
        {
            entity
                .HasOne(c => c.Author)
                .WithMany(a => a.Comments)
                .HasForeignKey(k => k.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
