using FietsRoute_Backend.Controllers;
using Business.Services;
using Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DefaultContext>();
var connectionString = builder.Configuration.GetConnectionString("TestConnection");
builder.Services.AddDbContext<DefaultContext>(options =>
{
    options.UseSqlServer(connectionString);
});
    
builder.Services.AddHttpClient(); 

builder.Services.AddScoped<WeatherService>(); 
builder.Services.AddScoped<RouteService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowedOrigins",
    policy =>
    {   
        policy.WithOrigins("http://localhost:4173"); // preview
        policy.WithOrigins("http://localhost:5173"); // dev
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyAllowedOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
