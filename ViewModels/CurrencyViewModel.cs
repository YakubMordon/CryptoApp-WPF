using CryptoApp.Models;
using System.ComponentModel;

namespace CryptoApp.ViewModels
{
    public class CurrencyViewModel : INotifyPropertyChanged
    {
        public CurrencyViewModel(CurrencyModel model)
        {
            Currency = model;
        }

        private CurrencyModel _currency;

        public CurrencyModel Currency
        {
            get => _currency;
            set
            {
                _currency = value;
                OnPropertyChanged(nameof(Currency));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
