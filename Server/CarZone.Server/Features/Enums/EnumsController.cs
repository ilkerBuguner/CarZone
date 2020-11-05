namespace CarZone.Server.Features.Enums
{
    using CarZone.Server.Features.Common;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static CarZone.Server.Infrastructure.ApiRoutes;

    public class EnumsController : ApiController
    {
        private readonly IEnumsService enumsService;

        public EnumsController(IEnumsService enumsService)
        {
            this.enumsService = enumsService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route(Enum.GetAll)]
        public ActionResult GetAll()
        {
            var enums = this.enumsService.GetAll();

            return this.Ok(enums);
        }
    }
}
