namespace btcusd_agregator_service.Interface
{
    public interface IPriceAggregatorStrategy
    {
        decimal Calculate(List<decimal> prices);
    }
}
