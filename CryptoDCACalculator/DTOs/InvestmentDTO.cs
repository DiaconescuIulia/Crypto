using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoDCACalculator.DTOs
{
    public class InvestmentDTO
    {
        public decimal ROI { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal CryptoAmount { get; set; }
    }
}
