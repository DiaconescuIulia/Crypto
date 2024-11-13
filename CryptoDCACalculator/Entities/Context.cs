using Microsoft.EntityFrameworkCore;

namespace CryptoDCACalculator.Entities
{
    public class Context: DbContext
    {
       public DbSet<Cryptocurrency> Cryptocurrencies { get; set; }
        public DbSet<CryptoPrice> CryptoPrices { get; set; }
        public DbSet<Investment> Investments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-DR11SC4\\SQLEXPRESS;Database=Crypto;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");

        }

        public Context(DbContextOptions<Context> options)
       : base(options)
        {
        }

    }
}
