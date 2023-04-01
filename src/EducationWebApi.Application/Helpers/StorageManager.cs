
namespace EducationWebApi.Application.Helpers;

public static class StorageManager
{
    public static string GetUrl(string storage, string path) => storage switch
    {
        "AzureStorage" => $"https://ibucloudstroage.blob.core.windows.net/{path}",
        "GoogleStorage" => $"https://storage.googleapis.com/{path}",
        "CloudinaryStorage" => $"https://res.cloudinary.com/base-link/{path}",
        _ => throw new ArgumentException("Invalid storage type")
    };
}