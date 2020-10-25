namespace CarZone.Server.Features.BrandModels
{
    using System.Threading.Tasks;

    using CarZone.Server.Data;
    using CarZone.Server.Data.Models;

    public class ModelsService : IModelsService
    {
        private readonly CarZoneDbContext data;

        public ModelsService(CarZoneDbContext data)
        {
            this.data = data;
        }

        public async Task<string> Create(string name, string brandId)
        {
            var brandModel = new Model
            {
                Name = name,
                BrandId = brandId,
            };

            this.data.Add(brandModel);

            await this.data.SaveChangesAsync();

            return brandModel.Id;
        }
    }
}
