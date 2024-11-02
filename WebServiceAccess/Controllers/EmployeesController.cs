using Microsoft.AspNetCore.Mvc;
using PrincipalObjects.Objects;

namespace WebServiceAccess.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : Controller
    {
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(ILogger<EmployeesController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetEmployees")]
        public async Task<IActionResult> GetEmployees() 
        {
            try
            {
                List<Employee> employeesToReturn = await new Employee().GetEmployees();

                if (employeesToReturn == null || employeesToReturn.Count == 0)
                {
                    return NotFound();
                }

                return Ok(employeesToReturn);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        [HttpGet("GetEmployeeById",Name = "GetEmployeeById")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            try
            {
                Employee employeeToReturn = await new Employee().GetEmployeeById(id);

                if (employeeToReturn == null)
                {
                    return NotFound();
                }

                return Ok(employeeToReturn);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }
    }
}
