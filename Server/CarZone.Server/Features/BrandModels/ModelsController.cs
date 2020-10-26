namespace CarZone.Server.Features.BrandModels
{
    using System.Threading.Tasks;

    using CarZone.Server.Features.BrandModels.Models;
    using CarZone.Server.Features.Common;
    using CarZone.Server.Features.Common.Models;
    using CarZone.Server.Infrastructure.Filters;
    using Microsoft.AspNetCore.Mvc;

    using static CarZone.Server.Infrastructure.ApiRoutes;

    [TypeFilter(typeof(IsAdminAuthorizationAttribute))]
    public class ModelsController : ApiController
    {
        private readonly IModelsService modelsService;

        public ModelsController(IModelsService modelsService)
        {
            this.modelsService = modelsService;
        }

        [HttpPost]
        [Route("[controller]")]
        public async Task<ActionResult> Create(CreateModelRequestModel model)
        {
            var brandModelId = await this.modelsService.Create(model.Name, model.BrandId);

            return Created(nameof(this.Create), brandModelId);
        }

        [HttpPut]
        [Route("[controller]/{modelId}")]
        public async Task<ActionResult> Update(string modelId, [FromBody]UpdateBrandModelRequestModel model)
        {
            var updateRequest = await this.modelsService
                .UpdateAsync(modelId, model.Name, model.BrandId);

            if (!updateRequest.Success)
            {
                return this.BadRequest(new ErrorsResponseModel
                {
                    Errors = updateRequest.Errors,
                });
            }

            return this.Ok();
        }
    }
}
