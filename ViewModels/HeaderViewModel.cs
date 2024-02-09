using CryptoApp.Models;
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
            header = new Header();

            NavigateHomeCommand = new RelayCommand(NavigateHome);
            NavigateToCryptoListCommand = new RelayCommand(NavigateToCryptoList);
            SearchButtonClickCommand = new RelayCommand(SearchButton_Click);

            //CurrentPage = new HomeView();
        }

        private Header header;

        // Commands for navigation
        public ICommand NavigateHomeCommand { get; }
        public ICommand NavigateToCryptoListCommand { get; }
        public ICommand SearchButtonClickCommand { get; }
        

        public Page CurrentPage
        {
            get { return header.CurrentPage; }
            set
            {
                header.CurrentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
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


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void TextBox_TextChanged()
        {
            PlaceholderVisibility = string.IsNullOrWhiteSpace(SearchText) ? Visibility.Visible : Visibility.Collapsed;
        }

        public void SearchButton_Click()
        {

        }

        public void NavigateHome()
        {

        }

        public void NavigateToCryptoList()
        {

        }
    }
}
