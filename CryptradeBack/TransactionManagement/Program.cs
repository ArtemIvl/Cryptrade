using Microsoft.EntityFrameworkCore;
using TransactionManagement.Data;
using TransactionManagement.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<TransactionService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = "Server=localhost;Database=transactiondb;User=root;Password=096-9503012;Port=3306";

// Configure the DbContext
builder.Services.AddDbContext<TransactionDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 1, 0)))); // Use the correct database provider (UseMySql in this case)


var app = builder.Build();

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

