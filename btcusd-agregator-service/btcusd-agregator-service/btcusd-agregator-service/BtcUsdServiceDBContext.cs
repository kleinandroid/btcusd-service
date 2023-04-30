using btcusd_agregator_service.Models;
using Microsoft.EntityFrameworkCore;

namespace btcusd_agregator_service
{
    public class BtcUsdServiceDBContext : DbContext
    {
        public BtcUsdServiceDBContext(DbContextOptions<BtcUsdServiceDBContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<BtcUsdAvaragePrice> Prices { get; set; }
    }
}
