using FietsRoute_Backend.Controllers;
using Business.Services;
using Data;
using Microsoft.EntityFrameworkCore;
using FietsRoute_Backend.Hubs;
using Google.Api;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using FietsRoute_Backend;
using Business;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "http://localhost:7237",
        ValidAudience = "http://localhost:5137",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("PesteringTOKENserving"))
    };
});
    


builder.Services.AddScoped<DefaultContext>();
var connectionString = builder.Configuration.GetConnectionString("TestConnection");
builder.Services.AddDbContext<DefaultContext>(options =>
{
    options.UseSqlServer(connectionString);
});
    
builder.Services.AddHttpClient();

builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

//builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
//builder.Services.AddSingleton<JwtSettings>();
builder.Services.AddScoped<JwtTokenService>();
builder.Services.AddScoped<AuthService>();
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
        policy.WithOrigins("http://localhost:5173"); // default dev
        policy.WithOrigins("http://localhost:5174"); // admin dev
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
