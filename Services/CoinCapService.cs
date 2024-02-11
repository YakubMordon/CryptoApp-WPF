using CryptoApp.Interfaces.Services;
using CryptoApp.Models;
using CryptoApp.ResponseData;
using Newtonsoft.Json;
using System.Net.Http;

namespace CryptoApp.Services
{
    /// <summary>
    /// Implementation for retrieving currency and candlestick data from CoinCap API.
    /// </summary>
    public class CoinCapService : ICoinCapService
    {
        /// <summary>
        /// HttpClient for making requests and working with responses
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoinCapService"/> class
        /// </summary>
        public CoinCapService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://api.coincap.io/v2/");
        }

        /// <summary>
        /// Implementation of method for retrieving a list of <see cref="CurrencyModel"/> with their <see cref="MarketModel"/> based on the provided search text and optional number limit.
        /// </summary>
        /// <param name="searchText">The text to search for in currency names.</param>
        /// <param name="number">The maximum number of <see cref="CurrencyModel"/> to retrieve. Defaults to 10 if not specified.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the list of <see cref="CurrencyModel"/>.</returns>
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

        /// <summary>
        /// Implementation of method for retrieving a list of <see cref="MarketModel"/> based on the provided currency Id.
        /// </summary>
        /// <param name="currencyId">The currency Id for filtering markets</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the list of <see cref="MarketModel"/>.</returns>
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

        /// <summary>
        /// Implementation of method for retrieving a list of <see cref="CandlestickModel"/> for a specific currency, exchange, and quote.
        /// </summary>
        /// <param name="currencyId">The unique identifier of the currency.</param>
        /// <param name="exchangeId">The unique identifier of the exchange.</param>
        /// <param name="quoteId">The unique identifier of the quote.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the list of <see cref="CandlestickModel"/>.</returns>
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
