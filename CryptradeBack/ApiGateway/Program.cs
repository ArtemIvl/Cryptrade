using ApiGateway;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddScoped<JwtTokenHandler>();
builder.Services.AddCustomJwtAuthentication();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhosts",
        builder => builder
            .WithOrigins("http://localhost:3000", "http://localhost:8002")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();
app.UseCors("AllowLocalhosts");
await app.UseOcelot();

app.UseAuthentication();
app.UseAuthorization();

app.Run();

