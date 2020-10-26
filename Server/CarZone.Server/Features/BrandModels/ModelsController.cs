namespace CarZone.Server.Features.BrandModels
{
    using System.Threading.Tasks;

    using CarZone.Server.Features.BrandModels.Models;
    using CarZone.Server.Features.Common;
    using CarZone.Server.Infrastructure.Filters;
    using Microsoft.AspNetCore.Mvc;

    [TypeFilter(typeof(IsAdminAuthorizationAttribute))]
    public class ModelsController : ApiController
    {
        private readonly IModelsService modelsService;

        public ModelsController(IModelsService modelsService)
        {
            this.modelsService = modelsService;
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateModelRequestModel model)
        {
            var brandModelId = await this.modelsService.Create(model.Name, model.BrandId);

            return Created(nameof(this.Create), brandModelId);
        }
    }
}
