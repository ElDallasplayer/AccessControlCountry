using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PrincipalObjects;
using PrincipalObjects.Objects;
using System.Net.Http;
using System.Text;

namespace AccessControlFront.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly string _webService;

        #region ILogger
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IConfiguration configuration)
        {
            Security.GenerateKey_Iv();

            _logger = logger;
            _configuration = configuration;
            _httpClient = new HttpClient();
            _webService = _configuration["WebService"];
        }
        #endregion

        public IActionResult Index()
        {
            CreateUserAPI(new User()
            {
                userName = "Admin",
                userPassword = Security.Encrypt("Delunoalocho"),
                userPermissions = 1,
                userRol = 1
            });

            return View();
        }

        public JsonResult LoginUser(string _user, string _password)
        {

            return Json(new { result = "OK" });
        }

        [HttpPost]
        public async Task<JsonResult> CreateUserAPI(User user)
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_webService}api/CreateUser", content);

            if (response.IsSuccessStatusCode)
            {
                var resultContent = await response.Content.ReadAsStringAsync();
                return Json(resultContent);
            }
            else
            {
                return Json(new { error = $"Error: {response.StatusCode}" });
            }
        }
    }
}
