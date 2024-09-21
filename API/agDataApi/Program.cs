using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Information);

// Configure Kestrel to listen for HTTP only (on port 7198)
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(7198); // HTTP only
});


// register the CustomerService for dependency injection
builder.Services.AddScoped<AgDataAPI.Services.ICustomerService, AgDataAPI.Services.CustomerService>();

// Add services to the container.
builder.Services.AddControllers();

// add the CORS policy to allow Angular app (running on localhost:4200)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAgDataAng",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")  // change this if we have to match the Angular app URL
                   .AllowAnyHeader()
                   .WithMethods("GET", "POST");  // we want to allow only GET and POST requests
        });
});

// Swagger/OpenAPI configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Application middleware configuration
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// lets enable CORS
app.UseCors("AllowAgDataAng");
app.UseAuthorization();
app.MapControllers();

app.Run();