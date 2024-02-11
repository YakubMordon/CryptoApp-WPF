namespace CryptoApp.ResponseData
{
    /// <summary>
    /// Represents the response data structure for candlestick information retrieved from CoinCap API.
    /// </summary>
    public class CandlestickResponse
    {
        /// <summary>
        /// List of <see cref="CandlestickDataResponse"/>.
        /// </summary>
        public List<CandlestickDataResponse> Data { get; set; }
    }

    /// <summary>
    /// Represents individual candlestick data response retrieved from CoinCap API.
    /// </summary>
    public class CandlestickDataResponse
    {
        /// <summary>
        /// Opening price of the candlestick.
        /// </summary>
        public double Open { get; set; }

        /// <summary>
        /// Highest price reached during the candlestick period.
        /// </summary>
        public double High { get; set; }

        /// <summary>
        /// Lowest price reached during the candlestick period.
        /// </summary>
        public double Low { get; set; }

        /// <summary>
        /// Closing price of the candlestick.
        /// </summary>
        public double Close { get; set; }

        /// <summary>
        /// Trading volume during the candlestick period.
        /// </summary>
        public double Volume { get; set; }

        /// <summary>
        /// Period of the candlestick.
        /// </summary>
        public long Period { get; set; }
    }
}
