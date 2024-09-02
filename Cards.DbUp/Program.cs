using Cards.DbUp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using NLog.Extensions.Logging;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Environment.ContentRootPath = Directory.GetCurrentDirectory();
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
LogManager.Configuration = new NLogLoggingConfiguration(builder.Configuration.GetSection("NLog"));
builder.Logging.AddNLog(); // Add NLog logging provider.
builder.Services.AddOptions(builder.Configuration); // Configure settings for IOptions DI.
builder.Services.AddServices(); // Register services for DI.
using IHost host = builder.Build();

var databaseMigrationService = host.Services.GetService<Cards.DbUp.Services.Abstractions.IDatabaseMigrationService>();
if (databaseMigrationService != null)
{
    databaseMigrationService.ApplyMSSQLDatabaseMigrations();
}

await host.RunAsync();