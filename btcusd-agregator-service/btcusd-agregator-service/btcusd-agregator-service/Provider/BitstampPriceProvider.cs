using btcusd_agregator_service.Interface;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace btcusd_agregator_service.Provider
{
    public class BitstampPriceProvider : IPriceProvider
    {
        const string currencyPair = "btcusd";
        public async Task<decimal> GetPriceAsync(DateTime timePoint)
        {
            string apiUrl = $"https://www.bitstamp.net/api/v2/ohlc/{currencyPair}/";
            string timeFrame = "3600";

            using HttpClient httpClient = new HttpClient();

            // Calculate the UNIX timestamp for the time-point
            long unixTimestamp = new DateTimeOffset(timePoint).ToUnixTimeSeconds();

            // Construct the request URL
            string requestUrl = $"{apiUrl}?step={timeFrame}&limit=1&start={unixTimestamp}";

            // Send a GET request to the API
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
