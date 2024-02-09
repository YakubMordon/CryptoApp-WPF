using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CryptoApp.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _inputText;
        public string InputText
        {
            get { return _inputText; }
            set
            {
                _inputText = value;
                OnPropertyChanged(nameof(InputText));
                // Implement input text logic here
            }
        }

        public ObservableCollection<string> SelectOptions { get; } // Populate this with options
        public string SelectedOption { get; set; } // Property for selected option

        public ObservableCollection<string> Elements { get; } // Populate this with elements
        public string ConverterResult { get; set; } // Property for converter result

        public HomeView()
        {
            InitializeComponent();

            // Initialize collections
            SelectOptions = new ObservableCollection<string>();
            Elements = new ObservableCollection<string>();

            // Populate SelectOptions and Elements collections
            // Implement logic to fetch/select options and elements
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Method to restrict input text to numbers only
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            // Implement logic to restrict input to numbers only
        }
    }
}
