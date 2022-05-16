using GeekGist;
using GeekGist.Services;
using GeekGist.Web;
using Microsoft.AspNetCore.Authentication.Cookies;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var cnn = config.GetConnectionString("GeekGistConnection");

Mapping.Config();

// 注册服务

builder.Services
    .AddAnet(opt => opt.EnableDefaultIdGen(0, 1, 5))
    .AddAnetDb<MySqlConnection>(cnn, opt =>
    {
        opt.EnableMetrics = true;
        opt.LogSensitiveData = true;
    })
    .AddAnetApi(withViews: true);

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
        new { Controller = "Book", Action = "Index", p = 1 });
    endpoints.MapControllerRoute("page", "page/{p:int}",
        new { Controller = "Book", Action = "Index" });
    endpoints.MapControllerRoute("book", "book/{id:int}",
       new { Controller = "Book", Action = "Detail" });
    endpoints.MapControllerRoute("sponsor", "sponsor",
       new { Controller = "Home", Action = "Sponsor" });
    endpoints.MapDefaultControllerRoute();
});

app.Run();
