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

        public async Task<List<CurrencyModel>> GetCurrencyModels(string? searchText, int number = 10)
        {
            string requestUrl = $"assets?limit={number}";

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                requestUrl += $"&search={searchText}";
            }

            var response = await _httpClient.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to fetch currency data from CoinCap API.");
            }

            var content = await response.Content.ReadAsStringAsync();
            var serializedCurrencyData = JsonConvert.DeserializeObject<CurrencyResponse>(content);

            var result = new List<CurrencyModel>();

            foreach (var item in serializedCurrencyData.Data)
            {
                
                result.Add(new CurrencyModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.PriceUsd,
                    PriceChange = item.ChangePercent24Hr,
                    Volume = item.VolumeUsd24Hr,
                    Markets = await GetCurrencyMarkets(item.Id)
                });
            }

            return result;
        }
        //Api: 963cc744-6a6c-419f-8a91-abbd34f3363a
        private async Task<List<MarketModel>> GetCurrencyMarkets(string currencyId)
        {
            var marketResponse = await _httpClient.GetAsync($"assets/{currencyId}/markets");

            if (!marketResponse.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to fetch market data for currency with ID {currencyId} from CoinCap API.");
            }

            var marketContent = await marketResponse.Content.ReadAsStringAsync();
            var serializedMarketData = JsonConvert.DeserializeObject<MarketResponse>(marketContent);

            var markets = serializedMarketData.Data.Select(item => new MarketModel
            {
                ExchangeId = item.ExchangeId,
                BaseId = item.BaseId,
                QuoteId = item.QuoteId,
                Price = item.PriceUsd
            }).GroupBy(m => m.ExchangeId)
                  .Select(g => g.First())
                  .ToList();


            return markets;
        }

        public async Task<List<CandlestickModel>> GetCandles(string currencyId, string exchangeId, string quoteId)
        {
            long currentTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            long yesterdayTimestamp = currentTimestamp - (24 * 60 * 60 * 1000);

            string requestString = $"candles?exchange={exchangeId}&baseId={currencyId}&quoteId={quoteId}&interval=h1&start={yesterdayTimestamp}&end={currentTimestamp}";

            var candleResponse = await _httpClient.GetAsync(requestString);

            if (!candleResponse.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to fetch candle data for currency with ID {currencyId} and exchange {exchangeId} from CoinCap API.");
            }

            var candleContent = await candleResponse.Content.ReadAsStringAsync();
            var serializedCandleData = JsonConvert.DeserializeObject<CandlestickResponse>(candleContent);

            var candles = serializedCandleData.Data.Select(item => new CandlestickModel
            {
                Open = item.Open,
                High = item.High,
                Low = item.Low,
                Close = item.Close,
                Volume = item.Volume,
                Period = item.Period
            }).ToList();

            return candles;
        }
    }
}
