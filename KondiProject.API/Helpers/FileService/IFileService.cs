namespace KondiProject.API.Helpers.FileService
{
    public interface IFileService
    {
        bool FormatIsValid(IFormFile file);

        bool FileIsExist(string fileName);

        Task<string> UploadFileAsync(IFormFile file);
    }
}
