using KondiProject.API.Enums;
using KondiProject.API.Models.Dtos.Responses;
using KondiProject.API.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using KondiProject.API.Helpers.HttpAccesor;

namespace KondiProject.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(policy: "UserPolicy")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IHttpAccessor _http;
        public UserController(IUserService userService, IHttpAccessor http)
        {
            _userService = userService;
            _http = http;
        }

        [HttpGet("me")]
        public async Task<ActionResult<UserResponse>> GetUserAsync()
        {
            var user = await _userService.GetByIdAsync(_http.GetUserId());
            if (user == null)
            {
                return NotFound(FunctionCode.UserNotFound);
            }
            return Ok(user);
        }
    }
}
