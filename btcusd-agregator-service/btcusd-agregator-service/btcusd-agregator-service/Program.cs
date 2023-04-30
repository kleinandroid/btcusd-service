using btcusd_agregator_service;
using btcusd_agregator_service.Interface;
using btcusd_agregator_service.Strategy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.TryAdd(ServiceDescriptor.Singleton<IMemoryCache, MemoryCache>());
builder.Services.AddSingleton<IPriceAggregatorStrategy, AveragePriceStrategy>(); // Register the default calculation avarage strategy
builder.Services.AddDbContext<BtcUsdServiceDBContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SqlLiteConnectionString")));
builder.Services.AddSingleton<IBtcUsdService, BtcUsdService>();
builder.Services.AddControllers(options =>
{
    options.ModelBinderProviders.Insert(0, new DateTimeModelBinderProvider(new[] { "M/d/yyyy h:mm:ss tt" }));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
