﻿using Microsoft.EntityFrameworkCore;
using PortfolioManagement.Data;
using PortfolioManagement.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<PortfolioService>();
builder.Services.AddScoped<RabbitMQConsumer>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddJsonFile("appsettings.json");

//var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
//var dbName = Environment.GetEnvironmentVariable("DB_NAME");
//var dbPassword = Environment.GetEnvironmentVariable("DB_ROOT_PASSWORD");

//var connectionString = $"server={dbHost};port=3306;database={dbName};user=root;password={dbPassword}";

// Configure the DbContext
builder.Services.AddDbContext<PortfolioDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection"), new MySqlServerVersion(new Version(8, 1, 0)))); // Use the correct database provider (UseMySql in this case)


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

// Use CORS
app.UseCors("AllowLocalhosts");
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

