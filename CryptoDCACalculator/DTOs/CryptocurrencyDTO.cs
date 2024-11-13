namespace CryptoDCACalculator.DTOs
{
  
        public class CryptocurrencyDTO
        {
            public Guid ID { get; set; }
            public string Name { get; set; }
            public decimal? LatestPrice { get; set; }
            public DateTime? LatestPriceTimestamp { get; set; }
    }
}
