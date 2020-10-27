namespace CarZone.Server.Features.Brands
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CarZone.Server.Data;
    using CarZone.Server.Features.Brands.Models;
    using CarZone.Server.Features.Common.Models;
    using Microsoft.EntityFrameworkCore;

    using static CarZone.Server.Features.Common.Constants;

    public class BrandsService : IBrandsService
    {
        private readonly CarZoneDbContext dbContext;

        public BrandsService(CarZoneDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ResultModel<IEnumerable<BrandListingServiceModel>>> GetAllAsync()
        {
            var brands = await this.dbContext
                .Brands
                .Select(b => new BrandListingServiceModel
                {
                    Id = b.Id,
                    Name = b.Name,
                })
                .ToListAsync();

            if (brands == null || brands.Count == 0)
            {
                return new ResultModel<IEnumerable<BrandListingServiceModel>>
                {
                    Errors = new string[] { Errors.NoBrandsFound },
                };
            }

            return new ResultModel<IEnumerable<BrandListingServiceModel>>
            {
                Success = true,
                Result = brands,
            };
        }
    }
}
