using CryptoDCACalculator.Entities;
using CryptoDCACalculator.DTOs;

namespace CryptoDCACalculator.Servicies.IServicies
{
    public interface ICryptocurrencyService
    {
        Task<List<CryptocurrencyDTO>> GetAllCryptocurrenciesAsync();
        Task<Cryptocurrency?> GetCryptocurrencyByIdAsync(Guid id);
        Task<List<CryptocurrencyInvestmentDTO?>> GetCryptocurrencyInvestmentsByIdsAsync(List<Guid> ids);
        Task Invest(List<InvestRequestDTO> investsRequest);
    }
}
