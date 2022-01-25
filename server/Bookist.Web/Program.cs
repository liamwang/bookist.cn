using Anet.Data;
using Bookist;
using Bookist.Services;
using Bookist.Web;
using Microsoft.AspNetCore.Authentication.Cookies;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
config.AddJsonFile("appsettings.Local.json", true, true);

var cnn = config.GetConnectionString("BookistConnection");

Mapping.Config();

// 注册服务

builder.Services
    .AddAnet(opt => opt.EnableDefaultIdGen(0, 1, 8))
    .AddDb<MySqlConnection>(DbDialect.MySQL, cnn, opt =>
    {
        opt.EnableMetrics = true;
        opt.LogSensitiveData = true;
    })
    .AddApi(withViews: true);

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        opt.LoginPath = "/account/login";
        opt.Events = new CookieAuthEvents();
    });

builder.Services.AddTransient<BookService>();
builder.Services.AddTransient<TagService>();

builder.Services.Configure<QiniuOptions>(config.GetSection("Qiniu"));
builder.Services.AddTransient<QiniuService>();

var app = builder.Build();

// 注册中间件

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();

app.UseAnetExceptionHandler("/api");

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
