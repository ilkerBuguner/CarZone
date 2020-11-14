namespace CarZone.Server.Features.Comforts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CarZone.Server.Data;
    using CarZone.Server.Features.Comforts.Models;
    using CarZone.Server.Features.Common.Models;
    using Microsoft.EntityFrameworkCore;

    using static CarZone.Server.Features.Common.Constants;

    public class ComfortsService : IComfortsService
    {
        private readonly CarZoneDbContext dbContext;

        public ComfortsService(CarZoneDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ResultModel<IEnumerable<ComfortListingServiceModel>>> GetAllAsync()
        {
            var comforts = await this.dbContext
                .Comforts
                .Select(c => new ComfortListingServiceModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    IsChecked = false,
                })
                .ToListAsync();

            if (comforts == null || comforts.Count == 0)
            {
                return new ResultModel<IEnumerable<ComfortListingServiceModel>>
                {
                    Errors = new string[] { Errors.NoComfortsFound }
                };
            }

            return new ResultModel<IEnumerable<ComfortListingServiceModel>>
            {
                Success = true,
                Result = comforts,
            };
        }
    }
}
