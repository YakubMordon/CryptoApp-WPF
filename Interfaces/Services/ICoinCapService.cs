using CryptoApp.Models;

namespace CryptoApp.Interfaces.Services
{
    interface ICoinCapService
    {
        Task<CurrencyModel> GetCurrencyData(string currencyId);
    }
}
