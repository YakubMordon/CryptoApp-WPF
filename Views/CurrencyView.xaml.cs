using System.Windows.Controls;
using CryptoApp.Models;
using CryptoApp.ViewModels;

namespace CryptoApp.Views
{
    /// <summary>
    /// Логика взаимодействия для CryptoInfo.xaml
    /// </summary>
    public partial class CurrencyView : UserControl
    {
        public CurrencyView(CurrencyModel model)
        {
            InitializeComponent();
            DataContext = new CurrencyViewModel(model);
        }
    }
}
