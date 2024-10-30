using Microsoft.AspNetCore.Mvc;
using PrincipalObjects.Objects;

namespace WebServiceAccess.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FamiliesController : Controller
    {
        [HttpGet("GetFamilyByEmpId")]
        public async Task<IActionResult> Index(int id)
        {
            try
            {
                List<Family> familyList = await new Family().GetFamilyByEmpId(id);
                return Ok(familyList.ToArray());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }
    }
}
