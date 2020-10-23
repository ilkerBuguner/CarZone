using CarZone.Server.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarZone.Server.Data.Configurations
{
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
