namespace btcusd_agregator_service.Interface
{
    public interface IPriceProvider
    {
        Task<decimal> GetPriceAsync(DateTime timePoint);
    }
}
