using Cards.Data.DependencyInjection;
using Cards.Api;
using NLog.Extensions.Logging;
using NLog;
using FluentValidation.AspNetCore;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Grouped Swagger Documentation for Yugioh.
    c.SwaggerDoc("Yugioh", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Yugioh API",
        Version = "v1"
    });

    // Define Swagger groups.
    c.DocInclusionPredicate((docName, apiDesc) =>
    {
        if (String.IsNullOrEmpty(apiDesc.GroupName))
            return true;  // Default grouping for controllers without GroupName.

        return docName.Equals(apiDesc.GroupName);
    });
});
LogManager.Configuration = new NLogLoggingConfiguration(builder.Configuration.GetSection("NLog"));
builder.Logging.AddNLog(); // Add NLog logging provider.
builder.Services.AddDataLayer(builder.Configuration); // Register the Data layer.
builder.Services.AddServices(); // Register common Api Services.
builder.Services.AddAutoMapper(typeof(Cards.Api.Mapping.Yugioh.CardMappingProfile)); // The actual profile here doesn't matter, just using it to find which assembly our mapping profiles are in.
builder.Services.AddValidatorsFromAssemblyContaining<Cards.Api.Validators.Yugioh.SetValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // Swagger endpoint for the Yugioh group.
        c.SwaggerEndpoint("/swagger/Yugioh/swagger.json", "Yugioh API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
