/*****************************************************
 * implement the service, which will take the user 
 * credentials as input and provide the token.
 * 1. Injecting the UserService.
 * 2. Specifies the “Post” request and uses “Login” as the route.
 * 3. Passing the User model as a parameter to the Login Method.
 * 4  Calling the Login method of User Service and passing the user model as a parameter.
 * 5. If the Service returns null or empty, it did not find the matching credentials to validate.
 * 6. Returning token upon successfully authenticating the user.
*****************************************************/

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Authenticetion;
using WebAPI.Services.Ineterface;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserServices _userService;
        public UserController(IUserServices userService)
        {
            // 1. Injecting the UserService.
            _userService = userService;
        }

        // 2. Specifies the “Post” request and uses “Login” as the route.
        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Login(UserModel user)              // 3. Passing the User model as a parameter to the Login Method.
        {
            var token = _userService.Login(user);              // 4  Calling the Login method of User Service and passing the user model as a parameter.
            if (token.IsNullOrEmpty())
            {
                //  5. If the Service returns null or empty, it did not find the matching credentials to validate.
                return BadRequest(new { message = "UserName or Password is incorrect" });
            }
            return Ok(token);                                  // 6. Returning token upon successfully authenticating the user.
        }

        [Authorize]
        [HttpGet("Demo")]
        public IActionResult Demo()
        {
            return Ok("User Authenticated Successfully!");
        }
    }
}
