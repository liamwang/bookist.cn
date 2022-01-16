using Bookist.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
config.AddJsonFile("appsettings.Local.json", true, true);

// 注册服务

builder.Services.AddControllersWithViews();

builder.Services.AddAnet()
    .AddDb<MySqlConnection>(config.GetConnectionString("BookistConnection"));

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(x => x.LoginPath = "/account/login");
builder.Services.AddTransient<BookService>();

var app = builder.Build();

// 注册中间件
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("home", "",
        new { controller = "Home", Action = "Index", pageNo = 1 });
    endpoints.MapControllerRoute("page", "page/{pageNo:int}",
        new { Controller = "Home", Action = "Index" });
    endpoints.MapDefaultControllerRoute();
});

app.Run();
