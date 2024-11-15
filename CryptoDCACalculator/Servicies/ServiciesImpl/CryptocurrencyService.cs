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

        public async Task<List<CryptocurrencyInvestmentDTO>> GetCryptocurrencyInvestmentsByIdsAsync(List<Guid> ids)
        {
          
            var investmentsRawData = await _context.Cryptocurrencies
                .Include(c => c.CryptoPrices)
                .Include(c => c.Investments)
                .Where(c => ids.Contains(c.ID))
                .ToListAsync();
            
            var cryptocurrencyInvestments = new List<CryptocurrencyInvestmentDTO>();
            
            foreach (var investmentRawData in investmentsRawData)
            {
                if (investmentRawData == null || !investmentRawData.Investments.Any())
                {
                    continue; 
                }

                var totalCrypto = investmentRawData.Investments.Sum(i => i.CryptoAmount);
                var actualCryptoValue = investmentRawData.CryptoPrices.MaxBy(cp => cp.Timestamp)?.Price ?? 0;
                var totalInvestment = investmentRawData.Investments.Sum(i => i.Amount);
                var profit = totalCrypto * actualCryptoValue - totalInvestment;
                var ROI = totalInvestment > 0 ? (profit / totalInvestment) : 0;
                
                var cryptocurrencyInvestmentDTO = new CryptocurrencyInvestmentDTO
                {
                    CryptoID = investmentRawData.ID,
                    CryptoName = investmentRawData.Name,
                    ROI = ROI,
                    CryptoInvestment = investmentRawData.Investments.Select(i => {
                        var profit = i.CryptoAmount * actualCryptoValue - i.Amount;
                        var ROI = (profit / i.Amount);
                        return new InvestmentDTO
                        {
                            Amount = i.Amount,
                            CryptoAmount = i.CryptoAmount,
                            Timestamp = i.Timestamp,
                            ROI = ROI
                        };
                    }),
                    CryptoPrices = investmentRawData.CryptoPrices
                };

                var latestPrice = investmentRawData.CryptoPrices.MaxBy(cp => cp.Timestamp)?.Price ?? 0;
                cryptocurrencyInvestmentDTO.CryptoCurrentValue = latestPrice;

                cryptocurrencyInvestments.Add(cryptocurrencyInvestmentDTO);
            }

            return cryptocurrencyInvestments;
        }

        public async Task Invest(List<InvestRequestDTO> investsRequest)
        {
            var cryptoPrices = await _context.CryptoPrices
                .Where(p => investsRequest.Select(r => r.CryptocurrencyID).Contains(p.CryptocurrencyID))
                .ToListAsync();

            var investmentEntities = new List<Investment>();

            investsRequest.ForEach(i =>
            {
                var investDate = i.StartDate.Value;
                var today = DateTime.UtcNow;
                while (investDate <= today)
                {
                    var currentCryptoPrice = cryptoPrices
                   .Where(p => p.CryptocurrencyID == i.CryptocurrencyID)
                   .OrderBy(p => p.Timestamp)
                   .First(p => p.Timestamp >= investDate);

                    var investmentEntity = new Investment
                    {
                        ID = Guid.NewGuid(),
                        CryptocurrencyID = i.CryptocurrencyID,
                        Amount = (decimal)i.InvestedAmount,
                        CryptoAmount = (decimal)(i.InvestedAmount / currentCryptoPrice.Price),
                        Timestamp = investDate,
                    };

                    investmentEntities.Add(investmentEntity);

                    investDate = investDate.AddMonths(1);
                }

            });

            _context.Investments.AddRange(investmentEntities);
            await _context.SaveChangesAsync();
        }
    }
}
