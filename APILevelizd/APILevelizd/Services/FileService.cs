using APILevelizd.Services.Interfaces;

namespace APILevelizd.Services;

public class FileService: IFileService
{
    private readonly IWebHostEnvironment _environment;

    public FileService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<string> SaveFileAsync(IFormFile imageFile, string[] allowedFileExtensions)
    {
        if (imageFile is null)
{                throw new ArgumentNullException(nameof(imageFile));}

        string contentPath = _environment.ContentRootPath;
        string path = Path.Combine(contentPath, "Uploads");
        
        if (!Path.Exists(path))
            Directory.CreateDirectory(path);

        string extension = Path.GetExtension(imageFile.FileName);

        if (!allowedFileExtensions.Contains(extension))
        {
            throw new ArgumentException($"Apenas arquivos do tipo {string.Join(",", allowedFileExtensions)}" +
                $" são permitidos.");
        }

        string imageName = $"{Guid.NewGuid().ToString()}{extension}";
        string imagePath = Path.Combine(path, imageName);

        using var stream = new FileStream(imagePath, FileMode.Create);
        await imageFile.CopyToAsync(stream);
        return imageName;
    }

    public void DeleteFile(string fileNameWithExtension)
    {
        if (fileNameWithExtension is null)
            throw new ArgumentNullException(nameof(fileNameWithExtension));

        string contentPath = _environment.ContentRootPath;
        string path = Path.Combine(contentPath, "Uploads");

        if (!Directory.Exists(path))
            throw new FileNotFoundException($"Caminho para o arquivo inválido");

        File.Delete(path);
    }
}
