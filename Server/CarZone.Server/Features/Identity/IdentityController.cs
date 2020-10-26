namespace CarZone.Server.Features.Identity
{
    using System.Threading.Tasks;

    using CarZone.Server.Data.Models;
    using CarZone.Server.Features.Common;
    using CarZone.Server.Features.Common.Models;
    using CarZone.Server.Features.Identity.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly AppSettings appSettings;
        private readonly IIdentityService identityService;

        public IdentityController(
            UserManager<User> userManager,
            IOptions<AppSettings> appSettings,
            IIdentityService identityService)
        {
            this.userManager = userManager;
            this.identityService = identityService;
            this.appSettings = appSettings.Value;
        }

        /// <summary>
        /// Register new User.
        /// </summary>
        /// <param name="input"></param>
        /// <response code="201"> Successfully registered user.</response>
        /// <response code="400"> Bad Reaquest.</response>
        /// <response code="401"> Unauthorized request.</response>
        [HttpPost]
        [Route(nameof(Register))]
        public async Task<ActionResult<AuthResponseModel>> Register([FromBody]RegisterRequestModel model)
        {
            var registerResult = await this.identityService.RegisterAsync(
                model.FullName,
                model.UserName,
                model.Email,
                model.Password,
                this.appSettings.Secret);

            if (!registerResult.Success)
            {
                return this.BadRequest(
                new ErrorsResponseModel
                {
                    Errors = registerResult.Errors,
                });
            }

            return registerResult.Result;
        }

        /// <summary>
        /// Log in User.
        /// </summary>
        /// <param name="input"></param>
        /// <response code="200"> Successfully logged in user.</response>
        /// <response code="400"> Bad Reaquest.</response>
        /// <response code="401"> Unauthorized request.</response>
        [HttpPost]
        [Route(nameof(Login))]
        public async Task<ActionResult<AuthResponseModel>> Login([FromBody]LoginRequestModel model)
        {
            var loginResult = await this.identityService.LoginAsync(model.UserName, model.Password, this.appSettings.Secret);

            if (!loginResult.Success)
            {
                return this.Unauthorized(new ErrorsResponseModel
                {
                    Errors = loginResult.Errors,
                });
            }

            return loginResult.Result;
        }
    }
}
