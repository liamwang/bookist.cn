using System.Text.RegularExpressions;

namespace Bookist;

public static class UrlUtil
{
    public static string ToSuffix(this ImageSize imageSize)
    {
        return $"!{imageSize.ToString().ToLower()}";
    }

    public static string Thumb(string fileKey)
    {
        return Image(fileKey, ImageSize.SM);
    }

    public static string Image(string fileKey, ImageSize? imageSize = ImageSize.MD)
    {
        var size = imageSize.HasValue ? imageSize.Value.ToSuffix() : "";
        return Config.ImageBaseUrl + fileKey + size;
    }

    public static string Domain(string url)
    {
         return new Uri(url).Host;
    }
}
