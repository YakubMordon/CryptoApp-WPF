using CryptoApp.Models;

namespace CryptoApp.Interfaces.Services
{
    /// <summary>
    /// Interface for retrieving currency and candlestick data from CoinCap API.
    /// </summary>
    interface ICoinCapService
    {
        /// <summary>
        /// Retrieves a list of <see cref="CurrencyModel"/> with their <see cref="MarketModel"/> based on the provided search text and optional number limit.
        /// </summary>
        /// <param name="searchText">The text to search for in currency names.</param>
        /// <param name="number">The maximum number of <see cref="CurrencyModel"/> to retrieve. Defaults to 10 if not specified.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the list of <see cref="CurrencyModel"/>.</returns>
        Task<List<CurrencyModel>> GetCurrencyModels(string? searchText, int number = 10);

        /// <summary>
        /// Retrieves a list of <see cref="CandlestickModel"/> for a specific currency, exchange, and quote.
        /// </summary>
        /// <param name="currencyId">The unique identifier of the currency.</param>
        /// <param name="exchangeId">The unique identifier of the exchange.</param>
        /// <param name="quoteId">The unique identifier of the quote.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the list of <see cref="CandlestickModel"/>.</returns>
        Task<List<CandlestickModel>> GetCandles(string currencyId, string exchangeId, string quoteId);
    }
}
