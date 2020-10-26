namespace CarZone.Server.Features
{
    using CarZone.Server.Features.Common;
    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]")]
    public class HomeController : ApiController
    {
        // [Authorize]
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("Works");
        }
    }
}
