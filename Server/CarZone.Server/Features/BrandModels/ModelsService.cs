namespace CarZone.Server.Features.BrandModels
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CarZone.Server.Data;
    using CarZone.Server.Data.Models;
    using CarZone.Server.Features.Common.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualBasic.CompilerServices;

    using static CarZone.Server.Features.Common.Constants;

    public class ModelsService : IModelsService
    {
        private readonly CarZoneDbContext dbContext;

        public ModelsService(CarZoneDbContext data)
        {
            this.dbContext = data;
        }

        public async Task<string> Create(string name, string brandId)
        {
            var brandModel = new Model
            {
                Name = name,
                BrandId = brandId,
            };

            this.dbContext.Add(brandModel);

            await this.dbContext.SaveChangesAsync();

            return brandModel.Id;
        }

        public async Task<ResultModel<bool>> UpdateAsync(string id, string name, string brandId)
        {
            var model = await this.GetByIdAsync(id);

            if (model == null)
            {
                return new ResultModel<bool>
                {
                    Errors = new string[] { Errors.InvalidModelId },
                };
            }

            model.Name = name;
            model.BrandId = brandId;
            model.ModifiedOn = DateTime.UtcNow;

            await this.dbContext.SaveChangesAsync();

            return new ResultModel<bool>
            {
                Success = true,
            };
        }

        public async Task<Model> GetByIdAsync(string id)
        {
            return await this.dbContext
            .Models
            .Where(m => m.Id == id)
            .FirstOrDefaultAsync();
        }
    }
}
