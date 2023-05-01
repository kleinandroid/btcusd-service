using btcusd_agregator_service.Interface;
using btcusd_agregator_service.Models;
using btcusd_agregator_service.Provider;
using Microsoft.Extensions.Caching.Memory;

namespace btcusd_agregator_service
{
    public class BtcUsdService : IBtcUsdService
    {
        private readonly IMemoryCache _cache;
        private readonly IEnumerable<IPriceProvider> _priceProviders;
        private readonly IPriceAggregatorStrategy _priceAggregatorStrategy;
        private readonly ITimePriceRepository _timePriceRepository;

        public BtcUsdService(IMemoryCache memoryCache, IPriceAggregatorStrategy priceAggregatorStrategy, ITimePriceRepository timePriceRepository)
        {
            _cache = memoryCache;
            _priceAggregatorStrategy = priceAggregatorStrategy;
            _timePriceRepository = timePriceRepository;

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
                decimal? storedPrice = await _timePriceRepository.GetPriceHourAsync(dateTime);
                if (storedPrice == null)
                {
                    // Fetch prices from all sources
                    var prices = new List<decimal>();
                    foreach (var priceProvider in _priceProviders)
                    {
                        prices.Add(await priceProvider.GetPriceAsync(dateTime));
                    }
                    averagePrice = CalculateAverage(prices);
                    TimePrice timePrice = new TimePrice() { TimePoint = dateTime, BtcUsdPrice = averagePrice };
                    await _timePriceRepository.SetPriceHourAsync(timePrice);
                    _cache.Set(hourKey, averagePrice);
                }
            }

            return averagePrice;
        }

        public async Task<IList<TimePrice>> GetPeriodAveragePricesAsync(DateTime startDate, DateTime endDate)
        {
            return await _timePriceRepository.GetPeriodPricesAsync(startDate, endDate);
        }

        private decimal CalculateAverage(List<decimal> prices)
        {
            return _priceAggregatorStrategy.Calculate(prices);
        }

    }
}
