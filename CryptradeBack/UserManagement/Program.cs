using Microsoft.EntityFrameworkCore;
using UserManagement.Data;
using JwtAuthenticationManager;
using UserManagement.Services;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<JwtTokenHandler>();
builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCustomJwtAuthentication();

// Configuration for database connection
//builder.Configuration.AddJsonFile("appsettings.json");

var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_ROOT_PASSWORD");

var connectionString = $"server={dbHost};port=3306;database={dbName};user=root;password={dbPassword}";
// Configure the DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, new  MySqlServerVersion(new Version(8, 1, 0)))); // Use the correct database provider (UseMySql in this case)

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost8002",
        builder => builder
            .WithOrigins("https://localhost:8002")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

//// Configure HTTPS with SSL certificate
//app.UseKestrel(options =>
//{
//    options.ListenAnyIP(443, listenOptions =>
//    {
//        var certPath = "/app/cert.pfx"; // Update with the correct path
//        var certPassword = "crypto"; // Update with the correct password
//        var cert = new X509Certificate2(certPath, certPassword);

//        listenOptions.UseHttps(cert);
//    });
//});

// Use CORS
app.UseCors("AllowLocalhost8002");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add middleware
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
