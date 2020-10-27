namespace CarZone.Server.Features.Comforts
{
    using System.Threading.Tasks;

    using CarZone.Server.Features.Common;
    using CarZone.Server.Features.Common.Models;
    using Microsoft.AspNetCore.Mvc;

    using static CarZone.Server.Infrastructure.ApiRoutes;

    public class ComfortsController : ApiController
    {
        private readonly IComfortsService comfortsService;

        public ComfortsController(IComfortsService comfortsService)
        {
            this.comfortsService = comfortsService;
        }

        [HttpGet]
        [Route(Comfort.GetAll)]
        public async Task<ActionResult> GetAll()
        {
            var allComfortsRequest = await this.comfortsService.GetAllAsync();

            if (!allComfortsRequest.Success)
            {
                return this.BadRequest(new ErrorsResponseModel
                {
                    Errors = allComfortsRequest.Errors,
                });
            }

            return this.Ok(allComfortsRequest.Result);
        }
    }
}
