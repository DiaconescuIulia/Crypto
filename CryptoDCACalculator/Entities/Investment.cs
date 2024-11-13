using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoDCACalculator.Entities
{
    public class Investment
    {
        public Guid ID { get; set; }
        public Guid CryptocurrencyID { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
        [Column(TypeName = "decimal(18,8)")]
        public decimal CryptoAmount { get; set; }
    }
}
