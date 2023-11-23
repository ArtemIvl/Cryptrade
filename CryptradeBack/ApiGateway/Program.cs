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
    options.AddPolicy("AllowLocalhost8002",
        builder => builder
            .WithOrigins("http://localhost:8002")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

// // Configure HTTPS with SSL certificate
// app.UseKestrel(options =>
// {
//     options.ListenAnyIP(443, listenOptions =>
//     {
//         var certPath = "/app/cert.pfx"; // Update with the correct path
//         var certPassword = "crypto"; // Update with the correct password
//         var cert = new X509Certificate2(certPath, certPassword);

//         listenOptions.UseHttps(cert);
//     });
// });

await app.UseOcelot();

// Use CORS
app.UseCors("AllowLocalhost8002");

app.UseAuthentication();
app.UseAuthorization();

app.Run();