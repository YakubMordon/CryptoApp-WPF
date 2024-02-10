using CryptoApp.Interfaces.Services;
using CryptoApp.Models;
using CryptoApp.ResponseData;
using Newtonsoft.Json;
using System.Net.Http;
using System.Windows.Controls;

namespace CryptoApp.Services
{
    public class CoinCapService : ICoinCapService
    {
        private readonly HttpClient _httpClient;

        public CoinCapService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://api.coincap.io/v2/");
        }

        public async Task<List<CurrencyModel>> GetCurrencyModels(int number = 10)
        {
            var response = await _httpClient.GetAsync($"assets");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to fetch currency data from CoinCap API.");
            }

            var content = await response.Content.ReadAsStringAsync();
            var serializedCurrencyData = JsonConvert.DeserializeObject<CurrencyResponse>(content);

            var result = new List<CurrencyModel>();

            var sortedList = serializedCurrencyData.Data
                                .OrderByDescending(currency => currency.Rank)
                                .Take(number); 

            foreach (var item in serializedCurrencyData.Data)
            {
                result.Add(new CurrencyModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = Decimal.Parse(item.PriceUsd),
                    PriceChange = Decimal.Parse(item.ChangePercent24Hr),
                    Volume = Decimal.Parse(item.VolumeUsd24Hr),
                    Markets = await GetCurrencyMarkets(item.Id)
                });
            }

            return result;
        }

        private async Task<List<MarketModel>> GetCurrencyMarkets(string currencyId)
        {
            var marketResponse = await _httpClient.GetAsync($"assets/{currencyId}/markets");

            if (!marketResponse.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to fetch market data for currency with ID {currencyId} from CoinCap API.");
            }

            var marketContent = await marketResponse.Content.ReadAsStringAsync();
            var serializedMarketData = JsonConvert.DeserializeObject<List<MarketDataResponse>>(marketContent);

            var markets = new List<MarketModel>();

            foreach (var item in serializedMarketData)
            {
                markets.Add(new MarketModel
                {
                    ExchangeId = item.ExchangeId,
                    QuoteId = item.QuoteId,
                    Price = item.PriceUsd,
                    Candles = await GetCandles(currencyId, item.ExchangeId)
                });
            }

            return markets;
        }

        private async Task<List<CandlestickModel>> GetCandles(string currencyId, string exchangeId)
        {
            long currentTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            long yesterdayTimestamp = currentTimestamp - (24 * 60 * 60 * 1000);

            string requestString = $"candles?exchange={exchangeId}&baseId={currencyId}&quoteId=USD&interval=h1&start={yesterdayTimestamp}&end={currentTimestamp}";

            var candleResponse = await _httpClient.GetAsync(requestString);

            if (!candleResponse.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to fetch candle data for currency with ID {currencyId} and exchange {exchangeId} from CoinCap API.");
            }

            var candleContent = await candleResponse.Content.ReadAsStringAsync();
            var serializedCandleData = JsonConvert.DeserializeObject<List<CandlestickDataResponse>>(candleContent);

            var candles = new List<CandlestickModel>();

            foreach (var item in serializedCandleData)
            {
                candles.Add(new CandlestickModel
                {
                    Open = item.Open,
                    High = item.High,
                    Low = item.Low,
                    Close = item.Close,
                    Volume = item.Volume,
                    Period = item.Period
                });
            }

            return candles;
        }
    }
}
