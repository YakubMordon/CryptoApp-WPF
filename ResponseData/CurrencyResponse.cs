namespace CryptoApp.ResponseData
{
    /// <summary>
    /// Represents the response data structure for currency information retrieved from CoinCap API.
    /// </summary>
    public class CurrencyResponse
    {
        /// <summary>
        /// List of <see cref="CurrencyDataResponse"/>.
        /// </summary>
        public List<CurrencyDataResponse> Data { get; set; }
    }

    /// <summary>
    /// Represents individual currency data response retrieved from CoinCap API.
    /// </summary>
    public class CurrencyDataResponse
    {
        /// <summary>
        /// Unique identifier of the currency.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Rank of the currency.
        /// </summary>
        public string? Rank { get; set; }

        /// <summary>
        /// Symbol of the currency.
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// Name of the currency.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Current supply of the currency.
        /// </summary>
        public decimal Supply { get; set; }

        /// <summary>
        /// Maximum supply of the currency.
        /// </summary>
        public decimal? MaxSupply { get; set; }

        /// <summary>
        /// Market capitalization of the currency in USD.
        /// </summary>
        public decimal? MarketCapUsd { get; set; }

        /// <summary>
        /// 24-Hour trading volume in USD.
        /// </summary>
        public decimal VolumeUsd24Hr { get; set; }

        /// <summary>
        /// Price of the currency in USD.
        /// </summary>
        public decimal PriceUsd { get; set; }

        /// <summary>
        /// Percentage change in price over the last 24 hours.
        /// </summary>
        public decimal ChangePercent24Hr { get; set; }

        /// <summary>
        /// Volume weighted average price over the last 24 hours.
        /// </summary>
        public decimal? Vwap24Hr { get; set; }
    }
}
