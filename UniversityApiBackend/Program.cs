//1. Usings to work with EntityFramework
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Models.DataModels;

var builder = WebApplication.CreateBuilder(args);

//2. COnnection with SQL Server Express
const string ConnectionName = "UniversityDb";
var connnectionString = builder.Configuration.GetConnectionString(ConnectionName);

//3. AddContext to services of builder
builder.Services.AddDbContext<UniversityDbContext>(options => options.UseSqlServer(connnectionString));


// Add services to the container.

builder.Services.AddControllers();
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

app.Run();
