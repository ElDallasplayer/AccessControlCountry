using Microsoft.AspNetCore.Mvc;
using PrincipalObjects.Objects;

namespace WebServiceAccess.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GlobalOptionsController : ControllerBase
    {
        private readonly ILogger<GlobalOptionsController> _logger;

        public GlobalOptionsController(ILogger<GlobalOptionsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetGlobalOptions")]
        public async Task<IActionResult> Get()
        {
            GlobalOptions options = await new GlobalOptions().GetGlobalOptions();
            return Ok(options);
        }
    }
}
