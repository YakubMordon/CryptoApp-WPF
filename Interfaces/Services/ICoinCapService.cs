using CryptoApp.Models;

namespace CryptoApp.Interfaces.Services
{
    interface ICoinCapService
    {
        Task<List<CurrencyModel>> GetCurrencyModels(string? searchText, int number = 10);
        Task<List<CandlestickModel>> GetCandles(string currencyId, string exchangeId, string quoteId);
    }
}
