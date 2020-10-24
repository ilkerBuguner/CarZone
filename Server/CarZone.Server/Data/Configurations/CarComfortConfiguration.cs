using CarZone.Server.Data.Models.Comfort;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CarZone.Server.Data.Configurations
{
    public class CarComfortConfiguration : IEntityTypeConfiguration<CarComfort>
    {
        public void Configure(EntityTypeBuilder<CarComfort> entity)
        {
            entity.HasKey(cc => new { cc.CarId, cc.ComfortId });

            entity
                .HasOne(cc => cc.Car)
                .WithMany(c => c.Comforts)
                .HasForeignKey(c => c.CarId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasOne(cc => cc.Comfort)
                .WithMany(c => c.CarComforts)
                .HasForeignKey(cc => cc.ComfortId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
