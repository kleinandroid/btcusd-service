using btcusd_agregator_service.Models;

namespace btcusd_agregator_service.Interface
{
    public interface ITimePriceRepository
    {
        Task<List<TimePrice>> GetPeriodPricesAsync(DateTime startTime, DateTime endTime);
        Task<decimal?> GetPriceHourAsync(DateTime specificHour);
        Task SetPriceHourAsync(TimePrice timePrice);
    }
}
