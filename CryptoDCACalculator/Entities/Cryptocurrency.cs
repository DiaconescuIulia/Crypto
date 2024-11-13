namespace CryptoDCACalculator.Entities
{
    public class Cryptocurrency
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public IEnumerable<CryptoPrice> CryptoPrices { get; set; }
        public IEnumerable<Investment> Investments { get; set; }
    }
}