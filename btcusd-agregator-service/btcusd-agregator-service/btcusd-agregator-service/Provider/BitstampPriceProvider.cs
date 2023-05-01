using btcusd_agregator_service.Interface;
using Newtonsoft.Json.Linq;

namespace btcusd_agregator_service.Provider
{
    public class BitstampPriceProvider : IPriceProvider
    {
        public async Task<decimal> GetPriceAsync(DateTime timePoint)
        {
            string apiUrl = $"https://www.bitstamp.net/api/v2/ohlc/btcusd/";
            using HttpClient httpClient = new HttpClient();
            long unixTimestampSecounds = new DateTimeOffset(timePoint).ToUnixTimeSeconds();
            var timeFrameSecounds = 60 * 60;
            string requestUrl = $"{apiUrl}?step={timeFrameSecounds}&limit=1&start={unixTimestampSecounds}";
            HttpResponseMessage response = await httpClient.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(jsonResponse);
                var closePrice = (decimal)json["data"]["ohlc"][0]["close"];

                //JObject closestDataPoint = (JObject)data[0];
                //decimal closePrice = decimal.Parse(closestDataPoint["close"].ToString());

                return closePrice;
            }
            else
            {
                throw new Exception($"Error fetching data from Bitstamp API: {response.StatusCode}");
            }
        }
    }
}
