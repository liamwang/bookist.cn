using Bookist.Web.Models;
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

var app = builder.Build();

// 注册中间件
app.UseStaticFiles();
app.MapDefaultControllerRoute();

app.Run();
