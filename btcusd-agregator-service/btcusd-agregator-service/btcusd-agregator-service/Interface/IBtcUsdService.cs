namespace btcusd_agregator_service.Interface
{
    public interface IBtcUsdService
    {
        Task<decimal> GetAveragePriceAsync(DateTime dateTime);
    }
}
