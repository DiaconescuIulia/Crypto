using CryptoDCACalculator.Entities;

namespace CryptoDCACalculator.Servicies.IServicies
{
    public interface ICryptocurrencyService
    {
        Task<List<Cryptocurrency>> GetAllCryptocurrenciesAsync();
        Task<Cryptocurrency?> GetCryptocurrencyByIdAsync(Guid id);
    }
}
