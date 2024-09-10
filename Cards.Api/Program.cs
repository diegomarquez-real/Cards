using Cards.Data.DependencyInjection;
using Cards.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDataLayer(builder.Configuration); // Register the Data layer.
builder.Services.AddServices(); // Register common Api Services.
builder.Services.AddAutoMapper(typeof(Cards.Api.Mapping.CardMappingProfile)); // The actual profile here doesn't matter, just using it to find which assembly our mapping profiles are in.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
