namespace CarZone.Server.Data.Configurations
{
    using CarZone.Server.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> entity)
        {
            entity
               .HasOne(c => c.Model)
               .WithMany(m => m.Cars)
               .HasForeignKey(k => k.ModelId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
