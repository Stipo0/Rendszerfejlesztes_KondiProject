using KondiProject.API.Enums;
using KondiProject.API.Models.Dtos.Requests;
using KondiProject.API.Services.GymService;
using KondiProject.API.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace KondiProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IGymService _gymService;

        public AuthController(IUserService userService, IGymService gymService)
        {
            _userService = userService;
            _gymService = gymService;
        }

        [HttpPost("user/register")]
        public async Task<ActionResult> RegisterAsync(UserRegisterRequest request)
        {
            if (await _userService.EmailIsExistAsync(request.Email))
            {
                return BadRequest(FunctionCode.EmailIsExist);
            }

            if (await _userService.UsernameIsExistAsync(request.Username))
            {
                return BadRequest(FunctionCode.UsernameIsExist);
            }

            await _userService.RegisterAsync(request);
            return Ok();
        }

        [HttpPost("user/login")]
        public async Task<ActionResult> UserLoginAsync(UserLoginRequest request)
        {
            var user = await _userService.GetByEmailAsync(request.Email);

            if (user == null || !_userService.VerifyLogin(user, request.Password))
            {
                return BadRequest(FunctionCode.WrongEmailOrPassword);
            }

            return Ok(_userService.CreateToken(user));
        }

        [HttpPost("gym/login")]
        public async Task<ActionResult> GymLoginAsync(GymLoginRequest request)
        {
            var gym = await _gymService.GetByEmailAsync(request.Email);

            if (gym == null || !_gymService.VerifyLogin(gym, request.Password))
            {
                return BadRequest(FunctionCode.WrongEmailOrPassword);
            }

            return Ok(_gymService.CreateToken(gym));
        }
    }
}
