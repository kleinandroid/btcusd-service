using btcusd_agregator_service;
using btcusd_agregator_service.Interface;
using btcusd_agregator_service.Strategy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

// Register the default calculation avarage strategy
builder.Services.AddSingleton<IPriceAggregatorStrategy, AveragePriceStrategy>();
builder.Services.AddSingleton<IBtcUsdService, BtcUsdService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
