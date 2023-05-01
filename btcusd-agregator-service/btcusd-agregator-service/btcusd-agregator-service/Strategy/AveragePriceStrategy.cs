using btcusd_agregator_service.Interface;

namespace btcusd_agregator_service.Strategy
{
    public class AveragePriceStrategy : IPriceAggregatorStrategy
    {
        public decimal Calculate(List<decimal> prices)
        {
            decimal sum = 0;
            foreach (var price in prices)
            {
                sum += price;
            }

            return sum / prices.Count;
        }
    }
}
