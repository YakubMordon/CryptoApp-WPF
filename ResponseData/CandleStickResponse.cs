namespace CryptoApp.ResponseData
{
    public class CandlestickResponse
    {
        public List<CandlestickDataResponse> Data { get; set; }
    }

    public class CandlestickDataResponse
    {
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double Volume { get; set; }
        public long Period { get; set; }
    }
}
