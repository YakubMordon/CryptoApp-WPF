using CryptoApp.Interfaces.Services;
using CryptoApp.Models;
using CryptoApp.Services;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace CryptoApp.ViewModels
{
    /// <summary>
    /// Delegate for changing page from <see cref="HeaderViewModel"/> to <see cref="HomeModel"/>
    /// </summary>
    /// <param name="value">Currency Model, which will be used to change page</param>
    public delegate void ChangePage(CurrencyModel value);

    /// <summary>
    /// View model for the Home view.
    /// </summary>
    class HomeViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Current <see cref="HomeModel"/>
        /// </summary>
        private readonly HomeModel _model;

        /// <summary>
        /// Delegate for changing page
        /// </summary>
        private ChangePage _changePage;

        /// <summary>
        /// Search bar text
        /// </summary>
        private string _searchText;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeViewModel"/> class.
        /// </summary>
        /// <param name="changePage">Delegate for changing the current page.</param>
        /// <param name="searchText">The search text.</param>
        public HomeViewModel(ChangePage changePage, string? searchText)
        {
            _model = new HomeModel();
            _changePage = changePage;
            _searchText = searchText;
            Initialize();
        }

        #region Commands

        /// <summary>
        /// Command to handle the click event on list items.
        /// </summary>
        public ICommand ListItemClickCommand => new RelayCommand<object>(ExecuteListItemClick);

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the input text.
        /// </summary>
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

        /// <summary>
        /// List of elements
        /// </summary>
        private ObservableCollection<string> elements;

        /// <summary>
        /// Gets or sets the list of elements.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the list of currency elements.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the list of cryptocurrency options.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the selected cryptocurrency from.
        /// </summary>
        public string SelectedCryptocurrencyFrom
        {
            get => _model.SelectedCryptocurrencyFrom;
            set
            {
                if (_model.SelectedCryptocurrencyFrom != value)
                {
                    _model.SelectedCryptocurrencyFrom = value;
                    OnPropertyChanged(nameof(SelectedCryptocurrencyFrom));
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected cryptocurrency to.
        /// </summary>
        public string SelectedCryptocurrencyTo
        {
            get => _model.SelectedCryptocurrencyTo;
            set
            {
                if (_model.SelectedCryptocurrencyTo != value)
                {
                    _model.SelectedCryptocurrencyTo = value;
                    OnPropertyChanged(nameof(SelectedCryptocurrencyTo));
                }
            }
        }

        /// <summary>
        /// Gets or sets the cryptocurrency input text.
        /// </summary>
        public string CryptocurrencyInputText
        {
            get => _model.CryptocurrencyInputText;
            set
            {
                if (_model.CryptocurrencyInputText != value)
                {
                    _model.CryptocurrencyInputText = value;
                    OnPropertyChanged(nameof(CryptocurrencyInputText));
                }
            }
        }

        /// <summary>
        /// Gets or sets the cryptocurrency output text.
        /// </summary>
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

        #endregion

        #region ActionHandlers

        /// <summary>
        /// Searches for <see cref="CurrencyModel"/> based on the input text and search text.
        /// </summary>
        private async Task Search()
        {
            ICoinCapService coinCapService = new CoinCapService();

            int.TryParse(InputText, out int number);

            List<CurrencyModel> currencyModels = await coinCapService.GetCurrencyModels(_searchText, number > 0 && number < 2001 ? number : 10);

            CurrencyElements = new ObservableCollection<CurrencyModel>(currencyModels);
        }

        /// <summary>
        /// Initializes the <see cref="HomeViewModel"/>.
        /// </summary>
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

        /// <summary>
        /// Executes when a list item is clicked.
        /// </summary>
        /// <param name="parameter">The parameter associated with the clicked item.</param>
        private void ExecuteListItemClick(object parameter)
        {
            if (parameter is string parameterString)
            {
                var separatedString = parameterString.Split("\t");

                var choosedModel = CurrencyElements.First(elem => elem.Id == separatedString[1].Trim());

                _changePage(choosedModel);
            }
        }

        /// <summary>
        /// Updates the list of elements.
        /// </summary>
        private void UpdateElementList()
        {
            var stringList = CurrencyElements.Select(item =>
                $"{item.Name.PadRight(20)}\t{item.Id.PadRight(32)}\t{item.Volume.ToString().PadRight(15)}\t{item.Price.ToString().PadRight(15)}\t{item.PriceChange.ToString().PadRight(15)}"
            ).ToList();

            Elements = new ObservableCollection<string>(stringList);
        }

        #endregion

        #region PropertyChanged

        /// <summary>
        /// Event for handling property change
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Method for handling property change
        /// </summary>
        /// <param name="propertyName">Name of property, which was changed</param>
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion
    }
}
