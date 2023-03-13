using AutoMapper;
using KondiProject.API.Enums;
using KondiProject.API.Models.Dtos.Requests;
using KondiProject.API.Models.Dtos.Responses;
using KondiProject.API.Services.GymService;
using KondiProject.API.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KondiProject.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(policy: "AdminPolicy")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IGymService _gymService;
        private readonly IMapper _mapper;

        public AdminController(IUserService userService, IGymService gymService, IMapper mapper)
        {
            _userService = userService;
            _gymService = gymService;
            _mapper = mapper;
        }

        [HttpGet("users")]
        public async Task<ActionResult<List<UserResponse>>> GetUsersAsync()
        {
            return _mapper.Map<List<UserResponse>>(await _userService.GetAllAsync());
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<UserResponse>> GetUserAsync(int id)
        {
            var user = _mapper.Map<UserResponse>(await _userService.GetByIdAsync(id));
            if (user == null)
            {
                return NotFound(FunctionCode.UserNotFound);
            }
            return Ok(user);
        }

        [HttpDelete("user/{id}")]
        public async Task<ActionResult> DeleteUserAsync(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound(FunctionCode.UserNotFound);
            }
            await _userService.DeleteAsync(user);
            return NoContent();
        }

        [HttpPost("gym/register")]
        public async Task<ActionResult> RegisterGymAsync(CreateGymRequest request)
        {
            if (await _gymService.EmailIsExistAsync(request.Email))
            {
                return BadRequest(FunctionCode.EmailIsExist);
            }

            if (await _gymService.NameIsExistAsync(request.Name))
            {
                return BadRequest(FunctionCode.NameIsExist);
            }

            if (await _gymService.PhoneNumberIsExistAsync(request.PhoneNumber))
            {
                return BadRequest(FunctionCode.PhoneNumberIsExist);
            }

            await _gymService.RegisterAsync(request);
            return Ok();
        }

        [HttpGet("gyms")]
        public async Task<ActionResult<List<GymResponse>>> GetGymsAsync()
        {
            return _mapper.Map<List<GymResponse>>(await _gymService.GetAllAsync());
        }

        [HttpGet("gym/{id}")]
        public async Task<ActionResult<GymResponse>> GetGymAsync(int id)
        {
            var gym = _mapper.Map<GymResponse>(await _gymService.GetByIdAsync(id));
            if (gym == null)
            {
                return NotFound(FunctionCode.GymNotFound);
            }
            return Ok(gym);
        }

        [HttpDelete("gym/{id}")]
        public async Task<ActionResult> DeleteGymAsync(int id)
        {
            var gym = await _gymService.GetByIdAsync(id);
            if (gym == null)
            {
                return NotFound(FunctionCode.GymNotFound);
            }
            await _gymService.DeleteAsync(gym);
            return NoContent();
        }
    }
}
