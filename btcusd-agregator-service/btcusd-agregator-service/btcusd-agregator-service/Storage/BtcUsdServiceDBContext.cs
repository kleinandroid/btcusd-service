using btcusd_agregator_service.Models;
using Microsoft.EntityFrameworkCore;

namespace btcusd_agregator_service.Storage
{
    public class BtcUsdServiceDBContext : DbContext
    {
        public BtcUsdServiceDBContext(DbContextOptions<BtcUsdServiceDBContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TimePrice>()
                .Property(x => x.TimePoint)
                .HasConversion(
                    v => v.Date.AddHours(v.Hour),
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                );
        }

        public DbSet<TimePrice> Prices { get; set; }
    }
}
