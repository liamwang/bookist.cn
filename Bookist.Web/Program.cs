var builder = WebApplication.CreateBuilder(args);

// 注册服务
builder.Services.AddRazorPages();

var app = builder.Build();

// 注册中间件
app.UseStaticFiles();
app.MapRazorPages();

app.Run();
