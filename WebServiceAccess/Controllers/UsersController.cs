using Microsoft.AspNetCore.Mvc;
using PrincipalObjects;
using PrincipalObjects.Objects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebServiceAccess.HttpModels;

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
            Security.GenerateKey_Iv();
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
                // Aquí necesitas definir la variable password
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
        public async Task<IActionResult> CreateUser([FromBody] HttpUserCreate request)
        {
            try
            {
                User userToReturn = await new User().InsertUserInDatabase(new User()
                    {
                        userName = request.UserName,
                        userPassword = Security.Encrypt(request.UserPassword),
                        userPermissions = request.Permissions,
                        userRol = request.Rol
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