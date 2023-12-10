using JwtAuthenticationManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddCustomJwtAuthentication();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost8000",
        builder => builder
            .WithOrigins("http://localhost:8000")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();
await app.UseOcelot();

// Use CORS
app.UseCors("AllowLocalhost8000");

app.UseAuthentication();
app.UseAuthorization();

app.Run();

