namespace CarZone.Server.Data.Configurations
{
    using CarZone.Server.Data.Models.Safety;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CarSafetyConfiguration : IEntityTypeConfiguration<CarSafety>
    {
        public void Configure(EntityTypeBuilder<CarSafety> entity)
        {
            entity.HasKey(k => new { k.CarId, k.SafetyId });

            entity
                .HasOne(cs => cs.Car)
                .WithMany(c => c.Safeties)
                .HasForeignKey(s => s.CarId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasOne(cs => cs.Safety)
                .WithMany(s => s.CarSafeties)
                .HasForeignKey(cs => cs.SafetyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
