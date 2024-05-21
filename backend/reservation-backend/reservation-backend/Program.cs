using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;
using reservation_backend.Database;
using reservation_backend.Interfaces;
using reservation_backend.Models;
using reservation_backend.Services;
using Serilog;
using Serilog.Extensions.Logging;
using Serilog.RequestAndResponseLogging;

var currentDate = DateTime.Now.ToString("yyyy-MM-dd");
var logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .CreateLogger();

logger.Information("Starting reservation api");

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));
var connectionString = builder.Configuration.GetConnectionString("SqliteConnection");
builder.Services.AddDbContext<Context>(options => options.UseSqlite(connectionString));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "specificOrigins",
        policy =>
        {
            policy.AllowCredentials();
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.WithOrigins("http://localhost:4200");
        });
});
builder.Services
    .AddAuthenticationCookie(validFor: TimeSpan.FromMinutes(15), options =>
    {
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Unauthorized/";
    })
    .AddAuthorization()
    .AddFastEndpoints().SwaggerDocument(o => o.AutoTagPathSegmentIndex = 0);
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IOSService, OSService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<INotificationService, NotificationService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseCors("specificOrigins");

app.UseHttpsRedirection();
app.UseAuthentication()
    .UseAuthorization()
    .UseFastEndpoints(x => x.Errors.UseProblemDetails())
    .UseSwaggerGen();
app.Run();
