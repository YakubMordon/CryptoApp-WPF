using CryptoApp.Models;

namespace CryptoApp.Interfaces.Services
{
    interface ICoinCapService
    {
        Task<List<CurrencyModel>> GetCurrencyModels(int number = 10);
    }
}
