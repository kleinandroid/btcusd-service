using btcusd_agregator_service.Models;

namespace btcusd_agregator_service.Interface
{
    public interface IBtcUsdService
    {
        Task<decimal> GetAveragePriceAsync(DateTime dateTime);
        Task<IList<TimePrice>> GetPeriodAveragePricesAsync(DateTime startDate, DateTime endDate);
    }
}
