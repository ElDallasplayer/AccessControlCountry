using Microsoft.AspNetCore.Mvc;
using PrincipalObjects.Objects;
using System.Linq.Expressions;

namespace WebServiceAccess.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StateController : Controller
    {
        [HttpGet("GetStates")]
        public async Task<IActionResult> Index()
        {
            List<State> statesToReturn = new List<State>();
            try
            {
                statesToReturn = await new State().GetStates();
                return Ok(statesToReturn);
            }catch(Exception ex) 
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }
    }
}
