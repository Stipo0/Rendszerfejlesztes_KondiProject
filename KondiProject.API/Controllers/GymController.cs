using AutoMapper;
using KondiProject.API.Enums;
using KondiProject.API.Helpers.FileService;
using KondiProject.API.Helpers.HttpAccesor;
using KondiProject.API.Models.Dtos.Requests;
using KondiProject.API.Models.Dtos.Responses;
using KondiProject.API.Services.GymMachineService;
using KondiProject.API.Services.GymService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KondiProject.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(policy: "GymPolicy")]
    [ApiController]
    public class GymController : ControllerBase
    {
        private readonly IGymService _gymService;
        private readonly IGymMachineService _machineService;
        private readonly IFileService _fileService;
        private readonly IHttpAccessor _http;
        private readonly IMapper _mapper;

        public GymController( IGymService gymService,
            IGymMachineService machineService,
            IFileService fileService,
            IHttpAccessor http,
            IMapper mapper)
        {
            _gymService = gymService;
            _machineService = machineService;
            _fileService = fileService;
            _http = http;
            _mapper = mapper;
        }

        [HttpPost("machine")]
        public async Task<ActionResult> CreateAsync([FromForm] CreateGymMachineRequest request)
        {
            if(request.Image != null && !_fileService.FormatIsValid(request.Image))
            {
                return BadRequest(FunctionCode.InvalidFileFormat);
            }

            await _machineService.CreateMachineAsync(_http.GetGymId(), request);
            return Ok();
        }

        [HttpGet("machines")]
        public async Task<List<GymMachineResponse>> GetAllByGymIdAsync()
        {
            return _mapper.Map<List<GymMachineResponse>>(await _machineService.GetAllByGymIdAsync(_http.GetGymId()));
        }

        [HttpGet("machine/{id}")]
        public async Task<GymMachineResponse> GetByIdAsync(int id)
        {
            return _mapper.Map<GymMachineResponse>(await _machineService.GetByIdAsync(id));
        }

        [HttpDelete("machine/{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var machine = await _machineService.GetByIdAsync(id);
            if(machine == null)
            { 
                return NotFound(FunctionCode.GymMachineNotFound); 
            }

            await _machineService.DeleteAsync(machine);
            return NoContent();
        }
    }
}
