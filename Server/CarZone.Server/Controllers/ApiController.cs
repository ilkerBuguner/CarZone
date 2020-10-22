using Microsoft.AspNetCore.Mvc;

namespace CarZone.Server.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public abstract class ApiController : ControllerBase
    {
    }
}
