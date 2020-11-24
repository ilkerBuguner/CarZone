namespace CarZone.Server.Features.Users
{
    using System.Threading.Tasks;

    using CarZone.Server.Features.Common;
    using CarZone.Server.Features.Common.Models;
    using CarZone.Server.Features.Users.Models;
    using CarZone.Server.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : ApiController
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpPut]
        [Route(Infrastructure.ApiRoutes.User.Update)]
        public async Task<ActionResult> Update(string userId, [FromBody] UpdateUserRequestModel model)
        {
            var updateRequest = await this.usersService.UpdateAsync(userId, model);

            if (!updateRequest.Success)
            {
                return this.BadRequest(new ErrorsResponseModel
                {
                    Errors = updateRequest.Errors,
                });
            }

            return this.Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route(Infrastructure.ApiRoutes.User.Details)]
        public async Task<ActionResult> Details(string userId)
        {
            var detailsRequest = await this.usersService.DetailsAsync(userId);

            if (!detailsRequest.Success)
            {
                return this.BadRequest(new ErrorsResponseModel
                {
                    Errors = detailsRequest.Errors,
                });
            }

            return this.Ok(detailsRequest.Result);
        }
    }
}
