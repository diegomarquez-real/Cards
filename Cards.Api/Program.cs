using Cards.Data.DependencyInjection;
using Cards.Api;
using NLog.Extensions.Logging;
using NLog;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithAuth(); // Add Swagger with JWT Bearer token authentication.
builder.Services.AddAuthicationUsingJWT(builder.Configuration); // Add JWT Bearer token authentication.
LogManager.Configuration = new NLogLoggingConfiguration(builder.Configuration.GetSection("NLog"));
builder.Logging.AddNLog(); // Add NLog logging provider.
builder.Services.AddDataLayer(builder.Configuration); // Register the Data layer.
builder.Services.AddServices(); // Register common Api Services.
builder.Services.AddOptions(builder.Configuration); // Register common Api Options.
builder.Services.AddAutoMapper(typeof(Cards.Api.Mapping.Yugioh.CardMappingProfile)); // The actual profile here doesn't matter, just using it to find which assembly our mapping profiles are in.
builder.Services.AddValidatorsFromAssemblyContaining<Cards.Api.Validators.Yugioh.SetValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // Swagger endpoint for the Dbo group.
        c.SwaggerEndpoint("/swagger/Dbo/swagger.json", "Dbo API v1");
        // Swagger endpoint for the Yugioh group.
        c.SwaggerEndpoint("/swagger/Yugioh/swagger.json", "Yugioh API v1");       
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();