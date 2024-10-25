#region Dependencies
using Microsoft.AspNetCore.Mvc;
using PrincipalObjects;
using PrincipalObjects.HttpObjects;
using PrincipalObjects.Objects;
#endregion

namespace WebServiceAccess.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                List<User> usersToReturn = await new User().GetUsersFromDatabase();

                if (usersToReturn == null || usersToReturn.Count == 0)
                {
                    return NotFound();
                }

                return Ok(usersToReturn);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        [HttpPost("ValidateUser", Name = "ValidateUser")]
        public async Task<IActionResult> ValidateUser([FromBody] HttpUser request)
        {
            try
            {
                User userToReturn = await new User().ValidateUser(request.UserName, request.UserPassword);

                if (userToReturn == null)
                {
                    return NotFound();
                }

                return Ok(userToReturn);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        [HttpPost("CreateUser", Name = "CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] User request)
        {
            try
            {
                User userToReturn = await new User().InsertUserInDatabase(new User()
                    {
                        userName = request.userName,
                        passwordAsString = request.passwordAsString,
                        userPermissions = request.userPermissions,
                        userRol = request.userRol
                    });

                if (userToReturn == null)
                {
                    return NotFound();
                }

                return Ok(userToReturn);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }
    }
}