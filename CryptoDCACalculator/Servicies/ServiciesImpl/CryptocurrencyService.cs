using CryptoDCACalculator.Entities;
using CryptoDCACalculator.Servicies.IServicies;
using Microsoft.EntityFrameworkCore;
using CryptoDCACalculator.DTOs;
using Microsoft.EntityFrameworkCore.Update.Internal;


namespace CryptoDCACalculator.Servicies.ServiciesImpl
{
    public class CryptocurrencyService : ICryptocurrencyService
    {
        private readonly Context _context;

        public CryptocurrencyService(Context context)
        {
            _context = context;
        }

        public async Task<List<CryptocurrencyDTO>> GetAllCryptocurrenciesAsync()
        {
            var prices = _context.CryptoPrices.ToList();

            var crypto = _context.Cryptocurrencies.ToList();

            var dtos = crypto.Select(c =>
            {
                var latestPrice = prices.Where(p => p.CryptocurrencyID == c.ID)?.MaxBy(p => p.Timestamp);
                return new CryptocurrencyDTO
                {
                    ID = c.ID,
                    Name = c.Name,
                    LatestPrice = latestPrice?.Price,
                    LatestPriceTimestamp = latestPrice?.Timestamp,
                };
            }).ToList();

            return dtos;
        }

        public async Task<Cryptocurrency?> GetCryptocurrencyByIdAsync(Guid id)
        {
            return await _context.Cryptocurrencies
                .Include(c => c.CryptoPrices)
                .FirstOrDefaultAsync(c => c.ID == id);
        }

        public async Task<CryptocurrencyInvestmentDTO?> GetCryptocurrencyInvestmentsByIdAsync(Guid id)
        {
            var investmentRawData = await _context.Cryptocurrencies
                .Include(c => c.CryptoPrices)
                .Include(c => c.Investments)
                .FirstOrDefaultAsync(c => c.ID == id);

            var totalCrypto = investmentRawData.Investments.Sum(i => i.CryptoAmount);
            var actualCryptoValue = investmentRawData.CryptoPrices.MaxBy(cp => cp.Timestamp).Price;
            var totalInvestment = investmentRawData.Investments.Sum(i => i.Amount);
            var profit = totalCrypto * actualCryptoValue - totalInvestment;

            var ROI = (profit / totalInvestment) * 100;

            CryptocurrencyInvestmentDTO cryptocurrencyInvestmentDTO = new CryptocurrencyInvestmentDTO 
            { 
                CryptoID = investmentRawData.ID, 
                CryptoName = investmentRawData.Name, 
                ROI = ROI,
                CryptoInvestment = investmentRawData.Investments,
                CryptoPrices = investmentRawData.CryptoPrices
            };



            return cryptocurrencyInvestmentDTO;

        }
    }
}
