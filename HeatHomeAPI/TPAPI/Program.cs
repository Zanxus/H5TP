using Microsoft.EntityFrameworkCore;
using TPAPI.Application.Hubs;
using TPAPI.Application.Services;
using TPAPI.Domain.Interfaces.Repositories;
using TPAPI.Domain.Interfaces.Services;
using TPAPI.Infrastructure;
using TPAPI.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<HomeHeatDbContext>(x => x.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("TPAPI.Infrastructure")));

builder.Services.AddScoped<IHeatingUnitRepository, HeatingUnitRepository>();
builder.Services.AddScoped<ITemperatureRepository, TemperatureRepository>();
builder.Services.AddScoped<IHeatingUnitService, HeatingUnitService>();
builder.Services.AddScoped<ITemperatureService, TemperatureService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<TPHub>();
builder.Services.AddScoped<WebSocketService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSignalR();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseWebSockets(new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromSeconds(5)
}); ;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseHttpsRedirection();
app.MapHub<TPHub>("/SignalR");
app.UseAuthorization();

app.MapControllers();

app.Run();
