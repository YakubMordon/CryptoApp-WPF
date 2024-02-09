namespace CryptoApp.Models
{
    public class CurrencyModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Volume { get; set; }
        public decimal PriceChange { get; set; }
        public List<MarketModel> Markets { get; set; }
    }
}
