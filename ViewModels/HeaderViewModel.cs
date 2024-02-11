using CryptoApp.Interfaces.Services;
using CryptoApp.Models;
using CryptoApp.Services;
using CryptoApp.Views;
using CryptoApp.Views.UserControls;
using GalaSoft.MvvmLight.CommandWpf;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CryptoApp.ViewModels
{
    class HeaderViewModel : INotifyPropertyChanged
    {
        public HeaderViewModel()
        {
            header = new HeaderModel();

            CurrentPage = new HomeView(ChangeCurrentCurrency, SearchText);
        }

        private HeaderModel header;

        // Commands for navigation
        public ICommand NavigateHomeCommand => new RelayCommand(NavigateHome);
        public ICommand NavigateToCryptoListCommand => new RelayCommand(NavigateToCurrencyInfo);

        public UserControl CurrentPage
        {
            get { return header.CurrentPage; }
            set
            {
                header.CurrentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        public CurrencyModel? CurrentCurrencyModel
        {
            get { return header.CurrentCurrencyModel; }
            set
            {
                header.CurrentCurrencyModel = value;
            }
        }

        public Visibility PlaceholderVisibility
        {
            get { return header.PlaceholderVisibility; }
            set
            {
                header.PlaceholderVisibility = value;
                OnPropertyChanged(nameof(PlaceholderVisibility));
            }
        }

        public string SearchText
        {
            get { return header.SearchText; }
            set
            {
                header.SearchText = value;
                OnPropertyChanged(nameof(SearchText));
                TextBox_TextChanged();
            }
        }

        public void TextBox_TextChanged()
        {
            PlaceholderVisibility = string.IsNullOrWhiteSpace(SearchText) ? Visibility.Visible : Visibility.Collapsed;
        }

        public void NavigateHome()
        {
            CurrentPage = new HomeView(ChangeCurrentCurrency, SearchText);
        }

        public void ChangeCurrentCurrency(CurrencyModel model)
        {
            ICoinCapService coinCapService = new CoinCapService();

            var firstMarket = model.Markets[0];

            var initializeTask = coinCapService.GetCandles(firstMarket.BaseId, firstMarket.ExchangeId, firstMarket.QuoteId);

            initializeTask.ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    // Handle the exception
                    Exception exception = task.Exception?.InnerException ?? task.Exception;
                    Console.WriteLine($"Candles addition failed: {exception.Message}");
                }
                else
                {
                    model.Markets[0].Candles = task.Result;
                }
            });
             
            CurrentCurrencyModel = model;
            NavigateToCurrencyInfo();
        }

        public void NavigateToCurrencyInfo()
        {
            if(CurrentCurrencyModel is not null)
            {
                CurrentPage = new CurrencyView(CurrentCurrencyModel);
            }
            else
            {
                MessageBox.Show("This function will be available only after you will enter any Currency Information page", "Error box", MessageBoxButton.OK);
            }

            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
