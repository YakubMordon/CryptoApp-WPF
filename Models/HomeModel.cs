using System.Collections.ObjectModel;

namespace CryptoApp.Models
{
    class HomeModel
    {
        public string InputText { get; set; }
        public ObservableCollection<CurrencyModel> Elements { get; set; }
        public ObservableCollection<string> CryptocurrencyOptions { get; set; }
        public string SelectedCryptocurrencyFrom { get; set; }
        public string SelectedCryptocurrencyTo { get; set; }
        public string CryptocurrencyInputText { get; set; }
        public string CryptocurrencyOutputText { get; set; }
    }
}
