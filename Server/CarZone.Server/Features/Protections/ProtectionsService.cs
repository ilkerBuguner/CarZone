namespace CarZone.Server.Features.Protections
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CarZone.Server.Data;
    using CarZone.Server.Features.Common.Models;
    using CarZone.Server.Features.Protections.Models;
    using Microsoft.EntityFrameworkCore;

    using static CarZone.Server.Features.Common.Constants;

    public class ProtectionsService : IProtectionsService
    {
        private readonly CarZoneDbContext dbContext;

        public ProtectionsService(CarZoneDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ResultModel<IEnumerable<ProtectionListingServiceModel>>> GetAllAsync()
        {
            var protections = await this.dbContext
                .Protections
                .Select(p => new ProtectionListingServiceModel
                {
                    Id = p.Id,
                    Name = p.Name,
                })
                .ToListAsync();

            if (protections == null || protections.Count == 0)
            {
                return new ResultModel<IEnumerable<ProtectionListingServiceModel>>
                {
                    Errors = new string[] { Errors.NoProtectionsFound },
                };
            }

            return new ResultModel<IEnumerable<ProtectionListingServiceModel>>
            {
                Success = true,
                Result = protections,
            };
        }
    }
}
