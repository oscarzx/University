//1. Usings to work with EntityFramework
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Models.DataModels;
using UniversityApiBackend.Services;

var builder = WebApplication.CreateBuilder(args);

//2. COnnection with SQL Server Express
const string ConnectionName = "UniversityDb";
var connnectionString = builder.Configuration.GetConnectionString(ConnectionName);

//3. AddContext to services of builder
builder.Services.AddDbContext<UniversityDbContext>(options => options.UseSqlServer(connnectionString));

// 7. Add service of JWT Autorization
//TODO:
//builder.Services.AddJwtTokenServices(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();

//4. Add custom services (folder services)
builder.Services.AddScoped<IStudentsService, StudentsService>();
//TODO: Add the rest of services

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// 8. TODO: Configuration Swagger to take care of Autorization of JWT
builder.Services.AddSwaggerGen();

// 5. CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});

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

//6. Tell app to use CORS
app.UseCors("CorsPolicy");

app.Run();
