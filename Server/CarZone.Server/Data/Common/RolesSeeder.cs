using CarZone.Server.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using static CarZone.Server.Data.Common.Constants.Seeding;

namespace CarZone.Server.Data.Common
{
    internal class RolesSeeder : ISeeder
    {
        public async Task SeedAsync(CarZoneDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();

            await SeedRoleAsync(roleManager, AdministratorRoleName);
        }

        private static async Task SeedRoleAsync(RoleManager<Role> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var result = await roleManager.CreateAsync(new Role(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
