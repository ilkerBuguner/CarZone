namespace CarZone.Server.Features.Exteriors
{
    using System.Threading.Tasks;

    using CarZone.Server.Features.Common;
    using CarZone.Server.Features.Common.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static CarZone.Server.Infrastructure.ApiRoutes;

    public class ExteriorsController : ApiController
    {
        private readonly IExteriorsService exteriorsService;

        public ExteriorsController(IExteriorsService exteriorsService)
        {
            this.exteriorsService = exteriorsService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route(Exterior.GetAll)]
        public async Task<ActionResult> GetAll()
        {
            var allExteriorsRequest = await this.exteriorsService.GetAllAsync();

            if (!allExteriorsRequest.Success)
            {
                return this.BadRequest(new ErrorsResponseModel
                {
                    Errors = allExteriorsRequest.Errors,
                });
            }

            return this.Ok(allExteriorsRequest.Result);
        }
    }
}
