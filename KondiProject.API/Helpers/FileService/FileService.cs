namespace KondiProject.API.Helpers.FileService
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _folderPath = "wwwroot\\Uploads";

        public FileService(IWebHostEnvironment env)
        {

            _env = env;

        }

        public bool FormatIsValid(IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
            {
                return false;
            }

            return true;
        }

        public bool FileIsExist(string fileName)
        {
            var filePath = Path.Combine(_env.ContentRootPath, _folderPath, fileName);

            if (!File.Exists(filePath))
            {
                return false;
            }

            return true;
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var fileExt = Path.GetExtension(file.FileName);
            var fileName = $"{Path.GetRandomFileName()}{fileExt}";
            var path = Path.Combine(_env.ContentRootPath, _folderPath, fileName);
            using (var stream = System.IO.File.Create(path))
            {
                await file.CopyToAsync(stream);
            };

            return fileName;
        }
    }
}
