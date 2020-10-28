namespace CarZone.Server.Features.BrandModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CarZone.Server.Data;
    using CarZone.Server.Data.Models;
    using CarZone.Server.Features.BrandModels.Models;
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

            await this.dbContext.AddAsync(brandModel);
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

        public async Task<ResultModel<bool>> DeleteAsync(string id)
        {
            var brandModel = await this.GetByIdAsync(id);

            if (brandModel == null)
            {
                return new ResultModel<bool>
                {
                    Errors = new string[] { Errors.InvalidModelId }
                };
            }

            brandModel.IsDeleted = true;
            brandModel.DeletedOn = DateTime.UtcNow;

            this.dbContext.Models.Update(brandModel);

            await this.dbContext.SaveChangesAsync();

            return new ResultModel<bool>
            {
                Success = true,
            };
        }

        public async Task<ResultModel<BrandModelDetailsServiceModel>> GetDetailsAsync(string id)
        {
            var brandModel = await this.dbContext
                .Models
                .Where(m => m.Id == id)
                .Select(m => new BrandModelDetailsServiceModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    BrandId = m.BrandId,
                    BrandName = m.Brand.Name,
                    CreatedOn = m.CreatedOn.ToString("mm:HH dd/MM/yyyy"),
                })
                .FirstOrDefaultAsync();

            if (brandModel == null)
            {
                return new ResultModel<BrandModelDetailsServiceModel>
                {
                    Errors = new string[] { Errors.InvalidModelId }
                };
            }

            return new ResultModel<BrandModelDetailsServiceModel>
            {
                Success = true,
                Result = brandModel,
            };
        }

        public async Task<ResultModel<IEnumerable<BrandModelListingServiceModel>>> GetAllByBrandIdAsync(string brandId)
        {
            var brandModels = await this.dbContext
                .Models
                .Where(m => m.BrandId == brandId && m.IsDeleted == false)
                .Select(m => new BrandModelListingServiceModel()
                {
                    Id = m.Id,
                    Name = m.Name
                })
                .ToListAsync();

            if (brandModels == null)
            {
                return new ResultModel<IEnumerable<BrandModelListingServiceModel>>
                {
                    Errors = new string[] { Errors.InvalidBrandId },
                };
            }

            return new ResultModel<IEnumerable<BrandModelListingServiceModel>>
            {
                Success = true,
                Result = brandModels,
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
