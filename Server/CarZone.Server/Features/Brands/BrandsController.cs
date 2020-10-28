namespace CarZone.Server.Features.Brands
{
    using System.Threading.Tasks;

    using CarZone.Server.Features.Common;
    using CarZone.Server.Features.Common.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static CarZone.Server.Infrastructure.ApiRoutes;

    public class BrandsController : ApiController
    {
        private readonly IBrandsService brandsService;

        public BrandsController(IBrandsService brandsService)
        {
            this.brandsService = brandsService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route(Brand.GetAll)]
        public async Task<ActionResult> GetAll()
        {
            var allBrandsRequest = await this.brandsService.GetAllAsync();

            if (!allBrandsRequest.Success)
            {
                return this.BadRequest(new ErrorsResponseModel
                {
                    Errors = allBrandsRequest.Errors,
                });
            }

            return this.Ok(allBrandsRequest.Result);
        }
    }
}
