using btcusd_agregator_service.Interface;
using btcusd_agregator_service.Models;
using Microsoft.EntityFrameworkCore;

namespace btcusd_agregator_service.Storage
{
    public class TimePriceRepository : ITimePriceRepository, IDisposable
    {
        private bool _disposedValue;
        private BtcUsdServiceDBContext _context;
        public TimePriceRepository(BtcUsdServiceDBContext context)
        {
            _context = context;
        }

        async public Task<decimal?> GetPriceHourAsync(DateTime specificHour)
        {
            var timePrice = await _context.Prices.FirstOrDefaultAsync(f => f.TimePoint.Year == specificHour.Year &&
                                                                f.TimePoint.Month == specificHour.Month &&
                                                                f.TimePoint.Day == specificHour.Day &&
                                                                f.TimePoint.Hour == specificHour.Hour);
            return timePrice != null ? (decimal?)timePrice.BtcUsdPrice : null;
        }

        async public Task<List<TimePrice>> GetPeriodPricesAsync(DateTime startTime, DateTime endTime)
        {
            if (startTime >= endTime)
            {
                throw new ArgumentException("The startTime must be earlier than endTime.");
            }

            var timePrice = await _context.Prices.Where(f => f.TimePoint >= startTime && f.TimePoint <= endTime).ToListAsync();

            return timePrice;
        }

        async public Task SetPriceHourAsync(TimePrice timePrice)
        {
            await _context.Prices.AddAsync(timePrice);
            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    if (_context != null)
                    {
                        _context.Dispose();
                        _context = null;
                    }
                }
                _disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        ~TimePriceRepository()
        {
            Dispose(disposing: false);
        }
    }
}
