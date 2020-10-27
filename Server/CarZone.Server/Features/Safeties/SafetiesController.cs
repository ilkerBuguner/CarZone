namespace CarZone.Server.Features.Safeties
{
    using System.Threading.Tasks;

    using CarZone.Server.Features.Common;
    using CarZone.Server.Features.Common.Models;
    using Microsoft.AspNetCore.Mvc;

    using static CarZone.Server.Infrastructure.ApiRoutes;

    public class SafetiesController : ApiController
    {
        private readonly ISafetiesService safetiesService;

        public SafetiesController(ISafetiesService safetiesService)
        {
            this.safetiesService = safetiesService;
        }

        [HttpGet]
        [Route(Safety.GetAll)]
        public async Task<ActionResult> GetAll()
        {
            var allSafetiesRequest = await this.safetiesService.GetAllAsync();

            if (!allSafetiesRequest.Success)
            {
                return this.BadRequest(new ErrorsResponseModel
                {
                    Errors = allSafetiesRequest.Errors,
                });
            }

            return this.Ok(allSafetiesRequest.Result);
        }
    }
}
