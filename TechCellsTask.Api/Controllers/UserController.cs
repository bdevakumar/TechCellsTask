using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TechCellsTask.Core.Services;

namespace TechCellsTask.Api.Controllers
{
    /// <summary>
    /// Performs operations on the User table
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserService userService;

        /// <summary>
        /// Initializes new instances of dependencies that are used in this controller
        /// </summary>
        /// <param name="userService">An instances of UserService</param>
        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Checks if the username exists in the database
        /// </summary>
        /// <param name="username">Username of the User</param>
        /// <returns>If successful return OkResult with username, else BadRequestResult</returns>
        [HttpGet("Login")]
        public IActionResult Login(string username)
        {
            var user = userService.GetByUserName(username);
            if (user == null)
                return BadRequest(new { Message = "User not found" });

            return Ok(new { user.UserName });
        }
    }
}
