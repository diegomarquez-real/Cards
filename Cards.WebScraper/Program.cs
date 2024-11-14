using Cards.WebScraper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Environment.ContentRootPath = Directory.GetCurrentDirectory();
builder.Configuration.AddJsonFile("appsettings.json", optional: true);
builder.Services.AddOptions(builder.Configuration); // Configure settings for IOptions DI.
builder.Services.AddServices(); // Register services for DI.
using IHost host = builder.Build();

var yugiohService = host.Services.GetService<Cards.WebScraper.Services.Abstractions.IYugiohService>();
var progressService = host.Services.GetService<Cards.WebScraper.Services.Abstractions.IProgressService>();

if(yugiohService != null)
{
    yugiohService.AddCardsFull();
}

await host.RunAsync();