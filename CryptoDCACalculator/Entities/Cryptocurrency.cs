namespace CryptoDCACalculator.Entities
{
    public class Cryptocurrency
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public List<CryptoPrice> CryptoPrices { get; set; }
    }
}