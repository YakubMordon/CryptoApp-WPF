using CryptoApp.Models;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace CryptoApp.ViewModels
{
    public delegate void ChangePage(CurrencyModel value);

    class HomeViewModel : INotifyPropertyChanged
    {
        private readonly HomeModel _model;

        private ChangePage _changePage;
        private string _searchText;

        public HomeViewModel(ChangePage changePage, string? searchText)
        {
            _model = new HomeModel();
            _changePage = changePage;
            _searchText = searchText;
            Initialize();
        }

        public string InputText
        {
            get => _model.InputText;
            set
            {
                if (_model.InputText != value)
                {
                    _model.InputText = value;
                    OnPropertyChanged(nameof(InputText));
                    Search();
                }
            }
        }

        public ObservableCollection<string> Elements => _model.Elements;

        public ObservableCollection<string> CryptocurrencyOptions => _model.CryptocurrencyOptions;

        public string SelectedCryptocurrencyFrom
        {
            get => _model.SelectedCryptocurrencyFrom;
            set
            {
                if (_model.SelectedCryptocurrencyFrom != value)
                {
                    _model.SelectedCryptocurrencyFrom = value;
                    OnPropertyChanged(nameof(SelectedCryptocurrencyFrom));
                    Search();
                }
            }
        }

        public string SelectedCryptocurrencyTo
        {
            get => _model.SelectedCryptocurrencyTo;
            set
            {
                if (_model.SelectedCryptocurrencyTo != value)
                {
                    _model.SelectedCryptocurrencyTo = value;
                    OnPropertyChanged(nameof(SelectedCryptocurrencyTo));
                    Search();
                }
            }
        }

        public string CryptocurrencyInputText
        {
            get => _model.CryptocurrencyInputText;
            set
            {
                if (_model.CryptocurrencyInputText != value)
                {
                    _model.CryptocurrencyInputText = value;
                    OnPropertyChanged(nameof(CryptocurrencyInputText));
                    Convert();
                }
            }
        }

        public string CryptocurrencyOutputText
        {
            get => _model.CryptocurrencyOutputText;
            set
            {
                if (_model.CryptocurrencyOutputText != value)
                {
                    _model.CryptocurrencyOutputText = value;
                    OnPropertyChanged(nameof(CryptocurrencyOutputText));
                }
            }
        }

        private void Search()
        {
            // Implement search functionality here
        }

        private void Convert()
        {
            // Implement convert functionality here
        }

        private void Initialize()
        {

            _model.Elements = new ObservableCollection<string>();

            _model.CryptocurrencyOptions = new ObservableCollection<string>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
