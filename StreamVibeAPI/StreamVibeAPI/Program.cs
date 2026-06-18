using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using StreamVibeAPI.Business.Abstract;
using StreamVibeAPI.Business.Concrete;
using StreamVibeAPI.Business.Mapping;
using StreamVibeAPI.DAL.Abstract;
using StreamVibeAPI.DAL.Concrete;
using StreamVibeAPI.DAL.Context;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfile));

var connectionString = $"Server={Environment.GetEnvironmentVariable("DB_HOST")};" +
                       $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                       $"Trusted_Connection={Environment.GetEnvironmentVariable("Trusted_Connection")};" +
                       "TrustServerCertificate=True;";

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddScoped<IGenreRepository, GenreRepository>();        
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IPlanRepository, PlanRepository>();
builder.Services.AddScoped<IFaqRepository, FaqRepository>();

builder.Services.AddScoped<IGenreService, GenreManager>();
builder.Services.AddScoped<IFaqService, FaqManager>();
builder.Services.AddScoped<IPlanService, PlanManager>();
builder.Services.AddScoped<IDeviceService, DeviceManager>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");   

app.UseAuthorization();

app.MapControllers();

app.Run();
