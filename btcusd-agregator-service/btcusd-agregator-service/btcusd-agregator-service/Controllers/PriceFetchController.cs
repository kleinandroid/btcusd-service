using btcusd_agregator_service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace btcusd_agregator_service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PriceFetchController : ControllerBase
    {
        private readonly IBtcUsdService _btcUsdService;

        public PriceFetchController(IBtcUsdService btcUsdService)
        {
            _btcUsdService = btcUsdService;
        }

        [HttpGet(Name = "FetchPrice")]
        //[FromQuery][RegularExpression(@"^([01][0-9]|2[0-3])$")] string hour
        public async Task<decimal> Get()
        {
            DateTime specificDateTime = new DateTime(2023, 04, 1, 12, 30, 0);
            
            return await _btcUsdService.GetAveragePriceAsync(specificDateTime);
        }
    }
}
