using Microsoft.EntityFrameworkCore;

using UserManagement.Data;
using JwtAuthenticationManager;
using UserManagement.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<JwtTokenHandler>();
builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCustomJwtAuthentication();


// Configuration for database connection
builder.Configuration.AddJsonFile("appsettings.json");

// Configure the DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection"), new  MySqlServerVersion(new Version(8, 1, 0)))); // Use the correct database provider (UseMySql in this case)

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

// Add middleware
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
