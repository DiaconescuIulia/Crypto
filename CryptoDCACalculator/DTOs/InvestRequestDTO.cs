namespace CryptoDCACalculator.DTOs
{
    public class InvestRequestDTO
    {
        public Guid CryptocurrencyID { get; set; }
        public decimal? InvestedAmount { get; set; }
        public int? Frequency { get; set; }
        public DateTime? StartDate { get; set; }
    }
}
