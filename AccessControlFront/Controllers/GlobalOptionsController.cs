using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PrincipalObjects.HttpObjects;
using PrincipalObjects.Objects;
using System.Text;

namespace AccessControlFront.Controllers
{
    public class GlobalOptionsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly string _webService;

        #region ILogger
        private readonly ILogger<GlobalOptionsController> _logger;

        public GlobalOptionsController(ILogger<GlobalOptionsController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClient = new HttpClient();
            _webService = _configuration["WebService"];
        }
        #endregion

        public async Task<JsonResult> GetGlobalOptions()
        {
            var response = await _httpClient.GetAsync($"{_webService}GlobalOptions");

            if (response.IsSuccessStatusCode)
            {
                var resultContent = await response.Content.ReadAsStringAsync();
                return Json(resultContent);
            }
            else
            {
                return Json(new { goAppName = "Generic", goCompanyName = "Generic", goFirstOption = "Employee", goSecondOption = "Assistance", goThirdOption = "Access", goFourthOption = "Visit" });
            }
        }
    }
}
