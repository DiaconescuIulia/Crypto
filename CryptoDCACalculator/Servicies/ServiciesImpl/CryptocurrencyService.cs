using CryptoDCACalculator.Entities;
using CryptoDCACalculator.Servicies.IServicies;

namespace CryptoDCACalculator.Servicies.ServiciesImpl
{
    public class CryptocurrencyService : ICryptocurrencyService
    {
        private readonly Context _context;

        public CryptocurrencyService(Context context)
        {
            _context = context;
        }

        public async Task<List<Cryptocurrency>> GetAllCryptocurrenciesAsync()
        {
            return await _context.Cryptocurrencies
                .Include(c => c.CryptoPrices)
                .ToListAsync();
        }

        public Task<Cryptocurrency?> GetCryptocurrencyByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
