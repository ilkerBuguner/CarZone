namespace CarZone.Server.Features.BrandModels
{
    using System.Threading.Tasks;

    using CarZone.Server.Features.BrandModels.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ModelsController : ApiController
    {
        private readonly IModelsService modelsService;

        public ModelsController(IModelsService modelsService)
        {
            this.modelsService = modelsService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(CreateModelRequestModel model)
        {
            var brandModelId = await this.modelsService.Create(model.Name, model.BrandId);

            return Created(nameof(this.Create), brandModelId);
        }
    }
}
