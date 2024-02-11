namespace CryptoApp.Models
{
    /// <summary>
    /// Model for displaying Currency data
    /// </summary>
    public class CurrencyModel
    {
        /// <summary>
        /// Unique identifier of the currency.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of the currency.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Price of the currency in USD.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 24-Hour trading volume in USD.
        /// </summary>
        public decimal Volume { get; set; }

        /// <summary>
        /// Percentage change in price over the last 24 hours.
        /// </summary>
        public decimal PriceChange { get; set; }

        /// <summary>
        /// List of <see cref="MarketModel"/>, where currency can be bought
        /// </summary>
        public List<MarketModel> Markets { get; set; }
    }
}
