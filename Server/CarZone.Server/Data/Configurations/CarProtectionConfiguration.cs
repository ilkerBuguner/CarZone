namespace CarZone.Server.Data.Configurations
{
    using CarZone.Server.Data.Models.Protection;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CarProtectionConfiguration : IEntityTypeConfiguration<CarProtection>
    {
        public void Configure(EntityTypeBuilder<CarProtection> entity)
        {
            entity.HasKey(cp => new { cp.CarId, cp.ProtectionId });

            entity
                .HasOne(cp => cp.Car)
                .WithMany(c => c.Protections)
                .HasForeignKey(p => p.CarId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasOne(cp => cp.Protection)
                .WithMany(p => p.CarProtections)
                .HasForeignKey(cp => cp.ProtectionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
