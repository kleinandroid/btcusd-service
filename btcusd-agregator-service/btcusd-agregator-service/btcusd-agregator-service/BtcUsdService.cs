using btcusd_agregator_service.Interface;
using btcusd_agregator_service.Provider;
using Microsoft.Extensions.Caching.Memory;

namespace btcusd_agregator_service
{
    public class BtcUsdService : IBtcUsdService
    {
        private readonly IMemoryCache _cache;
        private readonly List<IPriceProvider> _priceProviders;
        private readonly IPriceAggregatorStrategy _priceAggregatorStrategy;

        public BtcUsdService(IMemoryCache memoryCache, IPriceAggregatorStrategy priceAggregatorStrategy)
        {
            _cache = memoryCache;
            _priceAggregatorStrategy = priceAggregatorStrategy;

            // Register the price providers
            _priceProviders = new List<IPriceProvider>
            {
                new BitstampPriceProvider(),
                new BitfinexPriceProvider()
            };
        }
        public async Task<decimal> GetAveragePriceAsync(DateTime dateTime)
        {
            var hourKey = dateTime.ToString("yyyyMMddHH");
            if (!_cache.TryGetValue(hourKey, out decimal averagePrice))
            {
                // Fetch prices from all sources
                var prices = new List<decimal>();
                foreach (var priceProvider in _priceProviders)
                {
                    prices.Add(await priceProvider.GetPriceAsync(dateTime));
                }

                // Calculate the average price
                averagePrice = CalculateAverage(prices);

                // Store the average price in the cache
                _cache.Set(hourKey, averagePrice, TimeSpan.FromHours(1));

// Store the average price in SQLite
//using var connection = new SqliteConnection("Data Source=prices.db");
//await connection.ExecuteAsync("INSERT INTO btc_usd_prices (hour_key, average_price) VALUES (@hourKey, @averagePrice)",
//    new { hourKey, averagePrice });
            }

            return averagePrice;
        }
        private decimal CalculateAverage(List<decimal> prices)
        {
            return _priceAggregatorStrategy.Calculate(prices);
        }

    }
}
