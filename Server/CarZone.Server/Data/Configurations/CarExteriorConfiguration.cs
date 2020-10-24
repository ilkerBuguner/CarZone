using CarZone.Server.Data.Models.Exterior;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarZone.Server.Data.Configurations
{
    public class CarExteriorConfiguration : IEntityTypeConfiguration<CarExterior>
    {
        public void Configure(EntityTypeBuilder<CarExterior> entity)
        {
            entity.HasKey(ce => new { ce.CarId, ce.ExteriorId });

            entity
                .HasOne(ce => ce.Car)
                .WithMany(c => c.Exteriors)
                .HasForeignKey(p => p.CarId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasOne(ce => ce.Exterior)
                .WithMany(c => c.CarExteriors)
                .HasForeignKey(ce => ce.ExteriorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
