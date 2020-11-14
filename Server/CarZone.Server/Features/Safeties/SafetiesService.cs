namespace CarZone.Server.Features.Safeties
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CarZone.Server.Data;
    using CarZone.Server.Features.Common.Models;
    using CarZone.Server.Features.Safeties.Models;
    using Microsoft.EntityFrameworkCore;

    using static CarZone.Server.Features.Common.Constants;

    public class SafetiesService : ISafetiesService
    {
        private readonly CarZoneDbContext dbContext;

        public SafetiesService(CarZoneDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ResultModel<IEnumerable<SafetyListingServiceModel>>> GetAllAsync()
        {
            var safeties = await this.dbContext
                .Safeties
                .Select(s => new SafetyListingServiceModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    IsChecked = false,
                })
                .ToListAsync();

            if (safeties == null || safeties.Count == 0)
            {
                return new ResultModel<IEnumerable<SafetyListingServiceModel>>
                {
                    Errors = new string[] { Errors.NoSafetiesFound },
                };
            }

            return new ResultModel<IEnumerable<SafetyListingServiceModel>>
            {
                Success = true,
                Result = safeties,
            };
        }
    }
}
