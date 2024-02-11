namespace CryptoApp.ResponseData
{
    /// <summary>
    /// Represents the response data structure for market information retrieved from CoinCap API.
    /// </summary>
    public class MarketResponse
    {
        /// <summary>
        /// List of <see cref="MarketDataResponse"/>.
        /// </summary>
        public List<MarketDataResponse> Data { get; set; }
    }

    /// <summary>
    /// Represents individual market data response retrieved from CoinCap API.
    /// </summary>
    public class MarketDataResponse
    {
        /// <summary>
        /// Unique identifier of the exchange.
        /// </summary>
        public string ExchangeId { get; set; }

        /// <summary>
        /// Unique identifier of the base currency.
        /// </summary>
        public string BaseId { get; set; }

        /// <summary>
        /// Unique identifier of the quote currency.
        /// </summary>
        public string QuoteId { get; set; }

        /// <summary>
        /// Symbol of the base currency.
        /// </summary>
        public string? BaseSymbol { get; set; }

        /// <summary>
        /// Symbol of the quote currency.
        /// </summary>
        public string? QuoteSymbol { get; set; }

        /// <summary>
        /// 24-Hour trading volume in USD.
        /// </summary>
        public decimal? VolumeUsd24Hr { get; set; }

        /// <summary>
        /// Price of the market in USD.
        /// </summary>
        public decimal PriceUsd { get; set; }

        /// <summary>
        /// Percentage of volume.
        /// </summary>
        public decimal? VolumePercent { get; set; }
    }
}
