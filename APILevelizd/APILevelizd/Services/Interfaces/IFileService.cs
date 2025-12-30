using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace APILevelizd.Services.Interfaces;

public interface IFileService
{
    Task<string> SaveFileAsync(IFormFile imageFile, string[] allowedFileExtensions);
    void DeleteFile(string fileNameWithExtension);
}
