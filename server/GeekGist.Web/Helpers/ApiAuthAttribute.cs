using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GeekGist.Web;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ApiAuthAttribute : Attribute, IAuthorizationFilter, IFilterFactory
{
    public bool IsReusable => false;
    //private Db _db;
    //public string AuthenticationSchemes { get; set; }
    //public string Policy { get; set; }
    //public string Roles { get; set; }

    public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
    {
        return new ApiAuthAttribute()
        {
            //_db = serviceProvider.GetService<Db>()
        };
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.Identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
        }
    }
}

