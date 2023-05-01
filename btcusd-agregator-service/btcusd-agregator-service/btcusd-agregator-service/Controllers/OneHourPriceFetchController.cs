using btcusd_agregator_service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;

namespace btcusd_agregator_service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OneHourPriceFetchController : ControllerBase
    {
        private readonly IBtcUsdService _btcUsdService;

        public OneHourPriceFetchController(IBtcUsdService btcUsdService)
        {
            _btcUsdService = btcUsdService;
        }

        [HttpGet(Name = "FetchPrice")]
        public async Task<decimal> Get([FromQuery][BindRequired][DefaultValue("4/1/2023 12:00:00 PM")] DateTime specificHour)
        {
            return await _btcUsdService.GetAveragePriceAsync(specificHour);
        }
    }
}
