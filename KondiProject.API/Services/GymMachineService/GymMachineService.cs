using AutoMapper;
using KondiProject.API.Helpers.FileService;
using KondiProject.API.Models.Domains;
using KondiProject.API.Models.Dtos.Requests;
using KondiProject.API.Repositories.GymMachineRepository;

namespace KondiProject.API.Services.GymMachineService
{
    public class GymMachineService : IGymMachineService
    {
        private readonly IGymMachineRepository _repository;
        private readonly IHttpContextAccessor _context;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private string url;
        public GymMachineService(IGymMachineRepository repository,IHttpContextAccessor context,IFileService fileService, IMapper mapper)
        {
            _repository = repository;
            _context = context;
            _fileService = fileService;
            _mapper = mapper;
            url = $"{_context.HttpContext!.Request.Scheme}://{_context.HttpContext.Request.Host}/api/File/";

        }
        public async Task CreateMachineAsync(int gymId, CreateGymMachineRequest request)
        {
            var machine = _mapper.Map<GymMachine>(request);
            machine.GymId = gymId;
            if(request.Image != null)
            {
                machine.ImageName = await _fileService.UploadFileAsync(request.Image);
            }

           await _repository.CreateAsync(machine);
        }

        public async Task<List<GymMachine>> GetAllByGymIdAsync(int gymId)
        {
            var result = await _repository.GetAllByGymIdAsync(gymId);
            foreach (var item in result)
            {
                if(item.ImageName != null) 
                {
                    item.ImageName = $"{url}{item.ImageName}";
                }
            }
            return result;
        }

        public async  Task<GymMachine?> GetByIdAsync(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if(result != null) 
            { 
                result.ImageName = $"{url}{result.ImageName}";
            }
            return result;
        }

        public async Task DeleteAsync(GymMachine machine)
        {
            await _repository.DeleteAsync(machine);
        }
    }
}
