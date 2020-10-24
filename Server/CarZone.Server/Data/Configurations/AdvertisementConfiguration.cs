namespace CarZone.Server.Data.Configurations
{
    using CarZone.Server.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class AdvertisementConfiguration : IEntityTypeConfiguration<Advertisement>
    {
        public void Configure(EntityTypeBuilder<Advertisement> entity)
        {
            entity
                .HasOne(c => c.Car)
                .WithOne(a => a.Advertisement)
                .HasForeignKey<Car>(k => k.AdvertisementId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
