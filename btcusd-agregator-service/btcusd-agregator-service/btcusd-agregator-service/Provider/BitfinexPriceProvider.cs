﻿using btcusd_agregator_service.Interface;
using btcusd_agregator_service.Models;
using Newtonsoft.Json;

namespace btcusd_agregator_service.Provider
{
    public class BitfinexPriceProvider : IPriceProvider
    {
        public async Task<decimal> GetPriceAsync(DateTime timePoint)
        {
            var startTimestampMillisecounds = new DateTimeOffset(timePoint).ToUnixTimeMilliseconds();
            var timeFrameMillisecounds = 60 * 60 * 1000;
            var endTimestampMillisecounds = startTimestampMillisecounds + timeFrameMillisecounds;

            var url = $"https://api-pub.bitfinex.com/v2/candles/trade:1h:tBTCUSD/hist?start={startTimestampMillisecounds}&end={endTimestampMillisecounds}&limit=1";
            using HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to retrieve price data. Status code: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();

            var candlesArray = JsonConvert.DeserializeObject<object[][]>(content);

            var candles = candlesArray.Select(c => new BitfinexCandle
            {
                Timestamp = DateTimeOffset.FromUnixTimeMilliseconds((long)c[0]),
                Open = Convert.ToDecimal(c[1]),
                Close = Convert.ToDecimal(c[2]),
                High = Convert.ToDecimal(c[3]),
                Low = Convert.ToDecimal(c[4]),
                Volume = Convert.ToDecimal(c[5])
            }).ToList();
            var candle = candles.FirstOrDefault();

            if (candle == null)
            {
                throw new Exception("No price data available for the specified time point.");
            }

            return candle.Close;
        }
    }
}
