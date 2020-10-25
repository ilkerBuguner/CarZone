namespace CarZone.Server.Features.BrandModels
{
    using System.Threading.Tasks;

    public interface IModelsService
    {
        public Task<string> Create(string name, string brandId);
    }
}
