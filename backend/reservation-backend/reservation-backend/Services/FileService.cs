using System.Net.NetworkInformation;

namespace reservation_backend.Services;

public class FileService
{
    public const string ImageFolderPath = "wwwroot/images";
    public static string SaveFile(IFormFile file, string folderPath)
    {
        var path = Path.Combine(folderPath, file.FileName);
        using var stream = new FileStream(path, FileMode.Create);
        file.CopyTo(stream);
        return path;
    }
}