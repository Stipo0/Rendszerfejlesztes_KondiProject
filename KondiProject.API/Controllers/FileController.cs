using KondiProject.API.Helpers.FileService;
using Microsoft.AspNetCore.Mvc;

namespace KondiProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly IFileService _fileService;
        public FileController(IWebHostEnvironment env, IFileService fileService)
        {
            _env = env;
            _fileService = fileService;
        }

        [HttpGet("{fileName}")]
        public async Task<ActionResult> GetFile(string fileName)
        {
            if (!_fileService.FileIsExist(fileName))
            {
                return NotFound();
            }

            var filePath = Path.Combine(_env.ContentRootPath, "wwwroot\\Uploads", fileName);
            var fileExt = Path.GetExtension(fileName);
            var fileStream = new FileStream(filePath, FileMode.Open);

            await Task.CompletedTask;

            switch (fileExt)
            {
                case ".jpg":
                    return File(fileStream, "image/jpg");
                case ".jpeg":
                    return File(fileStream, "image/jpeg");
                case ".png":
                    return File(fileStream, "image/png");
                default:
                    return BadRequest("Invalid file type.");
            }
        }

        [HttpGet("download/{fileName}")]
        public async Task<ActionResult> DownloadFile(string fileName)
        {
            if (!_fileService.FileIsExist(fileName))
            {
                return NotFound();
            }

            var filePath = Path.Combine(_env.ContentRootPath, "wwwroot\\Uploads", fileName);

            return File(await System.IO.File.ReadAllBytesAsync(filePath), "application/octet-stream");
        }
    }
}
