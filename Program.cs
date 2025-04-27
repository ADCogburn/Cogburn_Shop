using Cogburn_Shop.Data;
using Cogburn_Shop.Entities;
using Cogburn_Shop.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<ItemService>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Ensures that the database has all migrations applied on startup.

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var connection = dbContext.Database.GetDbConnection();
    connection.Open();

    using (var command = connection.CreateCommand())
    {
        command.CommandText = "IF DB_ID('CogburnShopDb') IS NULL CREATE DATABASE CogburnShopDb;";
        command.ExecuteNonQuery();
    }

    dbContext.Database.Migrate();
}

app.Run();
