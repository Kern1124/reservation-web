using System.Net.NetworkInformation;

namespace reservation_backend.Services;

public class FileService
{
    public const string ImageFolderPath = "wwwroot/images";
    public static string SaveFile(IFormFile file, string filename, string folderPath)
    {
        var path = Path.Combine(folderPath, filename);
        using var stream = new FileStream(path, FileMode.Create);
        file.CopyTo(stream);
        return path;
    }
}