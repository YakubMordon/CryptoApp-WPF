using System.Collections.ObjectModel;

namespace CryptoApp.Models
{
    /// <summary>
    /// Model for handling changes on Home Page
    /// </summary>
    class HomeModel
    {
        /// <summary>
        /// Input number for Top N currencies
        /// </summary>
        public string InputText { get; set; }

        /// <summary>
        /// List of <see cref="CurrencyModel"/> to display
        /// </summary>
        public ObservableCollection<CurrencyModel> Elements { get; set; }

        /// <summary>
        /// List of options for convertion from one currency to another
        /// </summary>
        public ObservableCollection<string> CryptocurrencyOptions { get; set; }

        /// <summary>
        /// Selected Currency from which conversion needs to be done
        /// </summary>
        public string SelectedCryptocurrencyFrom { get; set; }

        /// <summary>
        /// Selected Currency to which conversion needs to be done
        /// </summary>
        public string SelectedCryptocurrencyTo { get; set; }

        /// <summary>
        /// Currency value for convertion
        /// </summary>
        public string CryptocurrencyInputText { get; set; }

        /// <summary>
        /// Converted Currency value
        /// </summary>
        public string CryptocurrencyOutputText { get; set; }
    }
}
