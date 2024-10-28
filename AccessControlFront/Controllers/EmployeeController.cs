#region Dependencies
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PrincipalObjects.HttpObjects;
using PrincipalObjects.Objects;
using System.Text;
#endregion

namespace AccessControlFront.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly string _webService;

        #region ILogger
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClient = new HttpClient();
            _webService = _configuration["WebService"];
        }
        #endregion


        public IActionResult EmployeeList()
        {
            return View();
        }

        public async Task<IActionResult> GetEmployeeList()
        {
            var response = await _httpClient.GetAsync($"{_webService}Employees");
            List<Employee> employeeList = new List<Employee>();

            if (response.IsSuccessStatusCode)
            {
                var resultContent = await response.Content.ReadAsStringAsync();
                employeeList = JsonConvert.DeserializeObject<List<Employee>>(resultContent);
            }

            return View("~/Views/Components/Employee/Tables/_EmployeesTableView.cshtml", employeeList);
        }
    }
}
