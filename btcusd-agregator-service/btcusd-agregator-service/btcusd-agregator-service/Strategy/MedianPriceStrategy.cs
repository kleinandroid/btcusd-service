using btcusd_agregator_service.Interface;

namespace btcusd_agregator_service.Strategy
{
    public class MedianPriceStrategy : IPriceAggregatorStrategy
    {
        public decimal Calculate(List<decimal> prices)
        {
            var sortedPrices = new List<decimal>(prices);
            sortedPrices.Sort();

            int count = sortedPrices.Count;
            if (count % 2 == 0)
            {
                return (sortedPrices[count / 2 - 1] + sortedPrices[count / 2]) / 2;
            }
            else
            {
                return sortedPrices[count / 2];
            }
        }
    }
}
