using CarZone.Server.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarZone.Server.Data
{
    public class CarZoneDbContext : IdentityDbContext<User>
    {
        public CarZoneDbContext(DbContextOptions<CarZoneDbContext> options)
            : base(options)
        {
        }
    }
}
