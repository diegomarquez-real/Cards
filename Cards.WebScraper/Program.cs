using Cards.WebScraper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog;


HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Environment.ContentRootPath = Directory.GetCurrentDirectory();
builder.Configuration.AddJsonFile("appsettings.json", optional: true);
builder.Configuration.AddUserSecrets<Program>();
LogManager.Configuration = new NLogLoggingConfiguration(builder.Configuration.GetSection("NLog"));
builder.Logging.AddNLog();
builder.Services.AddOptions(builder.Configuration); // Configure settings for IOptions DI.
builder.Services.AddServices(builder.Configuration); // Register services for DI.
using IHost host = builder.Build();

var logger = host.Services.GetRequiredService<ILogger<Program>>();

try
{   
    logger.LogInformation("Starting application.");

    var sessionService = host.Services.GetService<Cards.WebScraper.Identity.Abstractions.ISessionService>();

    if (sessionService == null)
    {
        logger.LogError("Session service not found.");
        return;
    }

    await sessionService.LoginAsync(String.Empty, String.Empty);

    var yugiohService = host.Services.GetService<Cards.WebScraper.Services.Abstractions.IYugiohService>();

    if (yugiohService == null)
    {
        logger.LogError("Yugioh service not found."); 
        return;
    }

    logger.LogInformation("Attempting to add yugioh cards.");

    yugiohService.AddCardsFull();

    logger.LogInformation("Yugioh cards successfully added.");
}
catch (Exception ex)
{
    logger.LogError(ex, "Process failed.");
}

await host.RunAsync();