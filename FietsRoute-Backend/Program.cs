using FietsRoute_Backend.Controllers;
using Business.Services;
using Data;
using Microsoft.EntityFrameworkCore;
using FietsRoute_Backend.Hubs;
using Google.Api;

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

builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

builder.Services.AddScoped<WeatherService>(); 
builder.Services.AddScoped<RouteService>();
builder.Services.AddScoped<RouteServiceSimple>();
builder.Services.AddScoped<CityService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowedOrigins",
    policy =>
    {   
        policy.WithOrigins("http://localhost:4173"); // preview
        policy.WithOrigins("http://localhost:5173"); // dev
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowCredentials();
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

app.MapHub<ChatHub>("ChatHub");

app.MapControllers();

app.Run();
