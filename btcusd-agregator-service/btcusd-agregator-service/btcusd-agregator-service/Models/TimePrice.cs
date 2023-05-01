using System.ComponentModel.DataAnnotations;

namespace btcusd_agregator_service.Models
{
    public record TimePrice
    {
        [Key]
        public DateTime TimePoint { get; set; }
        public decimal BtcUsdPrice { get; set; }
    }
}
