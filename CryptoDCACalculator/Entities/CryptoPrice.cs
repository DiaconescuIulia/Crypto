namespace CryptoDCACalculator.Entities
{
    public class CryptoPrice
    {
        public Guid ID { get; set; }
        public decimal Price { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid CryptocurrencyID { get; set; }

    }
}
