#region Dependencies
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PrincipalObjects.Objects;
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

        public async Task<IActionResult> EditEmployee(int id)
        {
            var response = await _httpClient.GetAsync($"{_webService}Employees/GetEmployeeById?id={id}");
            Employee employee = new Employee();

            if (response.IsSuccessStatusCode)
            {
                var resultContent = await response.Content.ReadAsStringAsync();
                employee = JsonConvert.DeserializeObject<Employee>(resultContent);

                var familyResponse = await _httpClient.GetAsync($"{_webService}Families/GetFamilyByEmpId?id={id}");
                if (response.IsSuccessStatusCode)
                {
                    var resultFamilyContent = await response.Content.ReadAsStringAsync();
                    try
                    {
                        List<Family> empFamily = JsonConvert.DeserializeObject<List<Family>>(resultFamilyContent);
                    }
                    catch (Exception ex)
                    {
                        Family empFamily = JsonConvert.DeserializeObject<Family>(resultFamilyContent);
                    }
                }
            }

            return View("~/Views/Employee/EditEmployee.cshtml", employee);
        }
    }
}
