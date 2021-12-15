using Bookist.Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// 注册服务
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BookistDbContext>(opt =>
{
    var conStr = config.GetConnectionString("BookistConnection");
    opt.UseMySql(conStr, ServerVersion.Parse("8.0"));
});
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(x => x.LoginPath = "/account/login");

var app = builder.Build();

// 注册中间件
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("home", "",
        new { controller = "Home", Action = "Index", page = 1 });
    endpoints.MapControllerRoute("page", "page/{page}",
        new { Controller = "Home", Action = "Index" });
    endpoints.MapDefaultControllerRoute();
});

app.Run();
