namespace CarZone.Server.Features.BrandModels
{
    using System.Threading.Tasks;

    using CarZone.Server.Data.Models;
    using CarZone.Server.Features.Common.Models;

    public interface IModelsService
    {
        Task<string> Create(string name, string brandId);

        Task<ResultModel<bool>> UpdateAsync(string id, string name, string brandId);

        Task<Model> GetByIdAsync(string id);
    }
}
