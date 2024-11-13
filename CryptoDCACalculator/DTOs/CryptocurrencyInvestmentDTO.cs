using CryptoDCACalculator.Entities;

namespace CryptoDCACalculator.DTOs

{
    public class CryptocurrencyInvestmentDTO
    {
        public Guid CryptoID { get; set; }
        public string CryptoName { get; set; }
        public decimal CryptoCurrentValue { get; set; }
        public IEnumerable<CryptoPrice> CryptoPrices { get; set; }
        public IEnumerable<Investment> CryptoInvestment { get; set; }
        public decimal ROI { get; set; }
    }
}
