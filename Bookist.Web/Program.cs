var builder = WebApplication.CreateBuilder(args);

// 注册服务
// builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

var app = builder.Build();
// 注册中间件
app.UseStaticFiles();
// app.MapRazorPages();
app.MapDefaultControllerRoute();

app.Run();
