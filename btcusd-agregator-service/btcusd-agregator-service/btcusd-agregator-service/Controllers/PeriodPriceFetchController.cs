using btcusd_agregator_service.Interface;
using btcusd_agregator_service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;

namespace btcusd_agregator_service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PeriodPriceFetchController : ControllerBase
    {
        private readonly IBtcUsdService _btcUsdService;

        public PeriodPriceFetchController(IBtcUsdService btcUsdService)
        {
            _btcUsdService = btcUsdService;
        }

        [HttpGet(Name = "FetchPriceForPeriod")]
        public async Task<IList<TimePrice>> Get([FromQuery][BindRequired][DefaultValue("10/1/2022 12:00:00 PM")] DateTime startDate,
                                                [FromQuery][BindRequired][DefaultValue("4/1/2023 12:00:00 PM")] DateTime endDate)
        {
            return await _btcUsdService.GetPeriodAveragePricesAsync(startDate, endDate);
        }
    }
}