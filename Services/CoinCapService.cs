using CryptoApp.Interfaces.Services;
using CryptoApp.Models;
using CryptoApp.ResponseData;
using Newtonsoft.Json;
using System.Net.Http;

namespace CryptoApp.Services
{
    public class CoinCapService : ICoinCapService
    {
        private readonly HttpClient _httpClient;

        public CoinCapService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://api.coincap.io/v2/assets/");
        }

        public async Task<CurrencyModel> GetCurrencyData(string currencyId)
        {
            var response = await _httpClient.GetAsync($"{currencyId}");
            var marketResponse = await _httpClient.GetAsync($"{currencyId}/markets");

            if (response.IsSuccessStatusCode && marketResponse.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<CurrencyResponse>(content);
                // Map CoinCapResponse to Currency model
                return MapToCurrency(result);
            }
            else
            {
                throw new Exception("Failed to fetch currency data from CoinCap API.");
            }
        }

        private CurrencyModel MapToCurrency(CurrencyResponse response)
        {
            throw new Exception();
        }
    }
}
