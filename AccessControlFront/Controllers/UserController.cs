#region Dependencies
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PrincipalObjects;
using PrincipalObjects.HttpObjects;
using PrincipalObjects.Objects;
using System.Net.Http;
using System.Text;
#endregion

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
            _logger = logger;
            _configuration = configuration;
            _httpClient = new HttpClient();
            _webService = _configuration["WebService"];
        }
        #endregion

        public async Task<IActionResult> Index()
        {
            //await CreateUserAPI(new User()
            //{
            //    userName = "Admin",
            //    passwordAsString = "Admin",
            //    userPassword = null,
            //    userPermissions = 1,
            //    userRol = 1
            //});

            return View();
        }

        public async Task<JsonResult> LoginUser(string _user, string _password)
        {
            var json = JsonConvert.SerializeObject(new HttpUser() { UserName = _user, UserPassword = _password });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_webService}Users/ValidateUser", content);

            if (response.IsSuccessStatusCode)
            {
                var resultContent = await response.Content.ReadAsStringAsync();
                return Json(new { response = "OK", content = resultContent, message = "Login success" });
            }
            else
            {
                return Json(new { response = "ERROR", content = "", message = "The user do not exist", error = $"Error: {response.StatusCode}" });
            }
        }

        [HttpPost]
        public async Task<JsonResult> CreateUserAPI(User user)
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_webService}Users/CreateUser", content);

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
