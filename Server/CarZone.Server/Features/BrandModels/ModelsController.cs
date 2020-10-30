namespace CarZone.Server.Features.BrandModels
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;

    using CarZone.Server.Features.BrandModels.Models;
    using CarZone.Server.Features.Common;
    using CarZone.Server.Features.Common.Models;
    using CarZone.Server.Infrastructure.Filters;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static CarZone.Server.Infrastructure.ApiRoutes;

    public class ModelsController : ApiController
    {
        private readonly IModelsService modelsService;

        public ModelsController(IModelsService modelsService)
        {
            this.modelsService = modelsService;
        }

        [HttpPost]
        [TypeFilter(typeof(IsAdminAuthorizationAttribute))]
        [Route(Model.Create)]
        public async Task<ActionResult> Create(CreateModelRequestModel model)
        {
            var brandModelId = await this.modelsService.CreateAsync(model.Name, model.BrandId);

            return Created(nameof(this.Create), brandModelId);
        }

        [HttpPut]
        [TypeFilter(typeof(IsAdminAuthorizationAttribute))]
        [Route(Model.Update)]
        public async Task<ActionResult> Update(string modelId, [FromBody] UpdateBrandModelRequestModel model)
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

        [HttpDelete]
        [TypeFilter(typeof(IsAdminAuthorizationAttribute))]
        [Route(Model.Delete)]
        public async Task<ActionResult> Delete(string modelId)
        {
            var deleteRequest = await this.modelsService.DeleteAsync(modelId);

            if (!deleteRequest.Success)
            {
                return this.BadRequest(new ErrorsResponseModel
                {
                    Errors = deleteRequest.Errors,
                });
            }

            return this.Ok();
        }

        [HttpGet]
        [TypeFilter(typeof(IsAdminAuthorizationAttribute))]
        [Route(Model.GetDetails)]
        public async Task<ActionResult> Details(string modelId)
        {
            var detailsRequest = await this.modelsService.GetDetailsAsync(modelId);

            if (!detailsRequest.Success)
            {
                return this.BadRequest(new ErrorsResponseModel
                {
                    Errors = detailsRequest.Errors,
                });
            }

            return this.Ok(detailsRequest.Result);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route(Model.GetAllByBrandId)]
        public async Task<ActionResult<IEnumerable<BrandModelListingServiceModel>>> GetAllByBrandId([FromBody]GetAllByBrandIdRequestModel model)
        {
            var allModelsByBrandIdRequest = await this.modelsService.GetAllByBrandIdAsync(model.BrandId);

            if (!allModelsByBrandIdRequest.Success)
            {
                return this.BadRequest(new ErrorsResponseModel
                {
                    Errors = allModelsByBrandIdRequest.Errors,
                });
            }

            return this.Ok(allModelsByBrandIdRequest.Result);
        }
    }
}
