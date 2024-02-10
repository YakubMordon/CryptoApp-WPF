using CryptoApp.Interfaces.Services;
using CryptoApp.Models;
using CryptoApp.Services;
using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public ICommand ListItemClickCommand => new RelayCommand<object>(ExecuteListItemClick);

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

        public ObservableCollection<CurrencyModel> Elements => _model.Elements;

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

        private void ExecuteListItemClick(object parameter)
        {
            if (parameter is CurrencyModel clickedItem)
            {
                _changePage(clickedItem);
            }
        }

        private async Task Search()
        {
            ICoinCapService coinCapService = new CoinCapService();

            List<CurrencyModel> currencyModels;

            if(int.TryParse(InputText, out int number) && number is < 2001 and > -1)
            {
                currencyModels = await coinCapService.GetCurrencyModels(_searchText, number);
            }
            else
            {
                currencyModels = await coinCapService.GetCurrencyModels(_searchText);
            }

            _model.Elements = new ObservableCollection<CurrencyModel>(currencyModels);
        }

        private void Convert()
        {
            // Implement convert functionality here
        }

        private async Task Initialize()
        {
            ICoinCapService coinCapService = new CoinCapService();

            var currencyModels = await coinCapService.GetCurrencyModels(_searchText);

            _model.Elements = new ObservableCollection<CurrencyModel>(currencyModels);

            _model.CryptocurrencyOptions = new ObservableCollection<string>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
