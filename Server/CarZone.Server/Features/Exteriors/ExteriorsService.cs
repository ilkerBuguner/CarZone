namespace CarZone.Server.Features.Exteriors
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CarZone.Server.Data;
    using CarZone.Server.Features.Common.Models;
    using CarZone.Server.Features.Exteriors.Models;
    using Microsoft.EntityFrameworkCore;

    using static CarZone.Server.Features.Common.Constants;

    public class ExteriorsService : IExteriorsService
    {
        private readonly CarZoneDbContext dbContext;

        public ExteriorsService(CarZoneDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ResultModel<IEnumerable<ExteriorListingServiceModel>>> GetAllAsync()
        {
            var exteriors = await this.dbContext
                .Exteriors
                .Select(e => new ExteriorListingServiceModel
                {
                    Id = e.Id,
                    Name = e.Name,
                })
                .ToListAsync();

            if (exteriors == null || exteriors.Count == 0)
            {
                return new ResultModel<IEnumerable<ExteriorListingServiceModel>>
                {
                    Errors = new string[] { Errors.NoExteriorsFound },
                };
            }

            return new ResultModel<IEnumerable<ExteriorListingServiceModel>>
            {
                Success = true,
                Result = exteriors,
            };
        }
    }
}
