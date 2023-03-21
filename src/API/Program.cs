using API.Configurations;
using Infrastructure;
using Infrastructure.Common;
using Serilog;
using System.Text.Json.Serialization;
using MediatR;

StaticLogger.EnsureLoggerIsInitialized();
Log.Information("Starting Web API...");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.AddConfigurations();

// Add controllers with some extra options
builder.Services.AddControllers().AddJsonOptions(x =>
{
    // Serialize enums as strings in api responses (e.g. Role)
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

    // Ignore omitted parameters on models to enable optional params (e.g. User update)
    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

// Add infrastructure services
builder.Services.AddInfrastructure(builder.Configuration);

// Add mediator and mapper
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add Endpoint explorer and Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline.
// Use Swagger and it's UI
app.UseSwagger();
app.UseSwaggerUI();

// Use infrastructure services: middlware, etc...
app.UseInfrastructure(builder.Configuration);

// Enable HTTPS Redirection
app.UseHttpsRedirection();

// Enable authorization capabilities
app.UseAuthorization();

// Map controllers
app.MapControllers();

// All done - let's run the app / API
Log.Information("The Web API is now ready to accept incoming requests!");
app.Run();
