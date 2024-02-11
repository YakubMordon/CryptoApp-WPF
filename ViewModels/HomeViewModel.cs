using CryptoApp.Interfaces.Services;
using CryptoApp.Models;
using CryptoApp.Services;
using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
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

        private ObservableCollection<string> elements;

        public ObservableCollection<string> Elements
        {
            get => elements;
            set 
            {
                if (elements != value)
                {
                    elements = value;
                    OnPropertyChanged(nameof(Elements));
                }
            }
        }

        public ObservableCollection<CurrencyModel> CurrencyElements
        {
            get => _model.Elements;
            set
            {
                if (_model.Elements != value)
                {
                    _model.Elements = value;
                    OnPropertyChanged(nameof(CurrencyElements));
                    UpdateElementList();
                }
            }
        }

        public ObservableCollection<string> CryptocurrencyOptions
        {
            get => _model.CryptocurrencyOptions;
            set
            {
                if (_model.CryptocurrencyOptions != value)
                {
                    _model.CryptocurrencyOptions = value;
                    OnPropertyChanged(nameof(CryptocurrencyOptions));
                }
            }
        }

        public string SelectedCryptocurrencyFrom
        {
            get => _model.SelectedCryptocurrencyFrom;
            set
            {
                if (_model.SelectedCryptocurrencyFrom != value)
                {
                    _model.SelectedCryptocurrencyFrom = value;
                    OnPropertyChanged(nameof(SelectedCryptocurrencyFrom));
                    Convert();
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
                    Convert();
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

            CurrencyElements = new ObservableCollection<CurrencyModel>(currencyModels);
        }

        private void Convert()
        {
            // Implement convert functionality here
        }

        private void Initialize()
        {
            ICoinCapService coinCapService = new CoinCapService();

            Elements = new ObservableCollection<string>();
            CryptocurrencyOptions = new ObservableCollection<string>();

            var initializeTask = coinCapService.GetCurrencyModels(_searchText);

            initializeTask.ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    // Handle the exception
                    Exception exception = task.Exception?.InnerException ?? task.Exception;
                    Console.WriteLine($"Initialization failed: {exception.Message}");
                }
                else
                {
                    var currencyModels = task.Result;
                    CurrencyElements = new ObservableCollection<CurrencyModel>(currencyModels);
                }
            });

        }

        private void ExecuteListItemClick(object parameter)
        {
            if (parameter is string parameterString)
            {
                var separatedString = parameterString.Split("\t");

                var choosedModel = CurrencyElements.First(elem => elem.Id == separatedString[1].Trim());

                _changePage(choosedModel);
            }
        }

        private void UpdateElementList()
        {
            var stringList = CurrencyElements.Select(item =>
                $"{item.Name.PadRight(20)}\t{item.Id.PadRight(32)}\t{item.Volume.ToString().PadRight(15)}\t{item.Price.ToString().PadRight(15)}\t{item.PriceChange.ToString().PadRight(15)}"
            ).ToList();

            Elements = new ObservableCollection<string>(stringList);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
