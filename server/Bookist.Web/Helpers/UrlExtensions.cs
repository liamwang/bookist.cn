using Microsoft.AspNetCore.Mvc;

namespace Bookist.Web;

public static class UrlExtensions
{
    public static string Active(this IUrlHelper url, string path)
    {
        var currPath = url.ActionContext.HttpContext.Request.Path.Value.Trim('/');
        if (currPath.Equals(path.Trim('/'), StringComparison.OrdinalIgnoreCase))
            return "active";
        return string.Empty;
    }
}

