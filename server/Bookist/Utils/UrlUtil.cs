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

    public static string ParseSlug(string name, long id)
    {
        ArgumentNullException.ThrowIfNull(name);

        var slug = name.ToLower()
            .Replace("c#", "csharp")
            .Replace("c++", "cpp");

        slug = Regex.Replace(slug, @"[^0-9a-z]+", "-").Trim('-');

        return slug == "" ? id.ToString() : slug + "-" + id;
    }

    public static long ResolveIdInSlug(string slug)
    {
        ArgumentNullException.ThrowIfNull(slug);

        var splitIndex = slug.LastIndexOf('-');
        if (splitIndex == -1)
        {
            return long.Parse(slug);
        }

        return long.Parse(slug[(splitIndex + 1)..]);
    }
}
