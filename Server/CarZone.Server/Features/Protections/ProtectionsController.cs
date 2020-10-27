namespace CarZone.Server.Features.Protections
{
    using System.Threading.Tasks;

    using CarZone.Server.Features.Common;
    using CarZone.Server.Features.Common.Models;
    using Microsoft.AspNetCore.Mvc;

    using static CarZone.Server.Infrastructure.ApiRoutes;

    public class ProtectionsController : ApiController
    {
        private readonly IProtectionsService protectionsService;

        public ProtectionsController(IProtectionsService protectionsService)
        {
            this.protectionsService = protectionsService;
        }

        [HttpGet]
        [Route(Protection.GetAll)]
        public async Task<ActionResult> GetAll()
        {
            var allProtectionsRequest = await this.protectionsService.GetAllAsync();

            if (!allProtectionsRequest.Success)
            {
                return this.BadRequest(new ErrorsResponseModel
                {
                    Errors = allProtectionsRequest.Errors,
                });
            }

            return this.Ok(allProtectionsRequest.Result);
        }
    }
}
