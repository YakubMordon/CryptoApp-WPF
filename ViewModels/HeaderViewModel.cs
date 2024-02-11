using CryptoApp.Models;
using CryptoApp.Views;
using CryptoApp.Views.UserControls;
using GalaSoft.MvvmLight.CommandWpf;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CryptoApp.ViewModels
{
    /// <summary>
    /// View model for the header section of the application.
    /// </summary>
    class HeaderViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Current <see cref="HeaderModel"/>
        /// </summary>
        private HeaderModel header;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderViewModel"/> class.
        /// </summary>
        public HeaderViewModel()
        {
            header = new HeaderModel();
            CurrentPage = new HomeView(ChangeCurrentCurrency, SearchText);
        }

        #region Commands

        // Commands for navigation
        public ICommand NavigateHomeCommand => new RelayCommand(NavigateHome);
        public ICommand NavigateToCryptoListCommand => new RelayCommand(NavigateToCurrencyInfo);

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the current page displayed under the header.
        /// </summary>
        public UserControl CurrentPage
        {
            get { return header.CurrentPage; }
            set
            {
                header.CurrentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        /// <summary>
        /// Gets or sets the current currency model.
        /// </summary>
        public CurrencyModel? CurrentCurrencyModel
        {
            get { return header.CurrentCurrencyModel; }
            set
            {
                header.CurrentCurrencyModel = value;
            }
        }

        /// <summary>
        /// Gets or sets the visibility of the placeholder text.
        /// </summary>
        public Visibility PlaceholderVisibility
        {
            get { return header.PlaceholderVisibility; }
            set
            {
                header.PlaceholderVisibility = value;
                OnPropertyChanged(nameof(PlaceholderVisibility));
            }
        }

        /// <summary>
        /// Gets or sets the search text.
        /// </summary>
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

        #endregion

        #region ActionHandlers

        /// <summary>
        /// Handles the text changed event of the search bar.
        /// </summary>
        public void TextBox_TextChanged() => PlaceholderVisibility = string.IsNullOrWhiteSpace(SearchText) ? Visibility.Visible : Visibility.Collapsed;

        /// <summary>
        /// Navigates to the home page.
        /// </summary>
        public void NavigateHome() => CurrentPage = new HomeView(ChangeCurrentCurrency, SearchText);

        /// <summary>
        /// Changes the current <see cref="CurrencyModel"/> and navigates to the currency information page.
        /// </summary>
        /// <param name="model">The selected <see cref="CurrencyModel"/>.</param>
        public void ChangeCurrentCurrency(CurrencyModel model)
        {
            CurrentCurrencyModel = model;

            NavigateToCurrencyInfo();
        }

        /// <summary>
        /// Navigates to the currency information page.
        /// </summary>
        public void NavigateToCurrencyInfo()
        {
            if (CurrentCurrencyModel is not null)
                CurrentPage = new CurrencyView(CurrentCurrencyModel);
            else
                MessageBox.Show("This function will be available only after you will enter any Currency Information page", "Error box", MessageBoxButton.OK);
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
