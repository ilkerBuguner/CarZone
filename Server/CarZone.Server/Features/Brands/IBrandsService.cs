namespace CarZone.Server.Features.Brands
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CarZone.Server.Features.Brands.Models;
    using CarZone.Server.Features.Common.Models;

    public interface IBrandsService
    {
        Task<ResultModel<IEnumerable<BrandListingServiceModel>>> GetAllAsync();
    }
}
