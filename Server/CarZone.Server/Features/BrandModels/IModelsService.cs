namespace CarZone.Server.Features.BrandModels
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CarZone.Server.Data.Models;
    using CarZone.Server.Features.BrandModels.Models;
    using CarZone.Server.Features.Common.Models;

    public interface IModelsService
    {
        Task<string> Create(string name, string brandId);

        Task<ResultModel<bool>> UpdateAsync(string id, string name, string brandId);

        Task<ResultModel<bool>> DeleteAsync(string id);

        Task<Model> GetByIdAsync(string id);

        Task<ResultModel<BrandModelDetailsServiceModel>> GetDetailsAsync(string id);

        Task<ResultModel<IEnumerable<BrandModelListingServiceModel>>> GetAllByBrandIdAsync(string brandId);
    }
}
