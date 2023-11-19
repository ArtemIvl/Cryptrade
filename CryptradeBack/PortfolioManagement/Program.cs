using JwtAuthenticationManager;
using Microsoft.EntityFrameworkCore;
using PortfolioManagement.Data;
using PortfolioManagement.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<PortfolioService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCustomJwtAuthentication();

// Configure the DbContext
builder.Services.AddDbContext<PortfolioDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection"), new MySqlServerVersion(new Version(8, 1, 0)))); // Use the correct database provider (UseMySql in this case)


// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000",
        builder => builder
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

// Use CORS
app.UseCors("AllowLocalhost3000");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

