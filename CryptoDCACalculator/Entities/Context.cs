using Microsoft.EntityFrameworkCore;

namespace CryptoDCACalculator.Entities
{
    public class Context: DbContext
    {
        DbSet<Cryptocurrency> Cryptocurrencies { get; set; }
        DbSet<CryptoPrice> CryptoPrices { get; set; }

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
