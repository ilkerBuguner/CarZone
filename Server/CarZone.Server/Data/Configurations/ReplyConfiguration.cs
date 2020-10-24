namespace CarZone.Server.Data.Configurations
{
    using CarZone.Server.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
