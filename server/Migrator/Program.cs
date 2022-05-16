using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Migrator;

var host = Host.CreateDefaultBuilder(args);

host.ConfigureServices((builder, services) =>
{
    services.AddDbContext<MainDbContext>(opt =>
    {
        var conStr = builder.Configuration.GetConnectionString("GeekGistConnection");
        opt.UseMySql(conStr, ServerVersion.AutoDetect(conStr), o => o.MigrationsAssembly("Migrator"));
    });
    services.AddTransient<MockData>();
});

var app = host.Build();

var mockData = app.Services.GetService<MockData>();
mockData.Execute();