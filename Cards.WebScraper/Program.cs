using Cards.WebScraper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Environment.ContentRootPath = Directory.GetCurrentDirectory();
builder.Configuration.AddJsonFile("appsettings.json", optional: true);
builder.Configuration.AddUserSecrets<Program>();
builder.Services.AddOptions(builder.Configuration); // Configure settings for IOptions DI.
builder.Services.AddServices(builder.Configuration); // Register services for DI.
using IHost host = builder.Build();

try
{   
    var sessionService = host.Services.GetService<Cards.WebScraper.Identity.Abstractions.ISessionService>();

    if (sessionService == null)
        return;

    await sessionService.LoginAsync(String.Empty, String.Empty);

    var yugiohService = host.Services.GetService<Cards.WebScraper.Services.Abstractions.IYugiohService>();

    if (yugiohService == null)
        return;

    yugiohService.AddCardsFull();
}
catch (Exception ex)
{
    // TODO: Log exception.
}

await host.RunAsync();