using CarZone.Server.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarZone.Server.Data.Configurations
{
    public class ReplyConfiguration : IEntityTypeConfiguration<Reply>
    {
        public void Configure(EntityTypeBuilder<Reply> entity)
        {
            entity
                .HasOne(r => r.Author)
                .WithMany(a => a.Replies)
                .HasForeignKey(k => k.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasOne(r => r.RootComment)
                .WithMany(c => c.Replies)
                .HasForeignKey(k => k.RootCommentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
