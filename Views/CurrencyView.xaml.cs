using System.Windows.Controls;
using CryptoApp.Models;
using CryptoApp.ViewModels;

namespace CryptoApp.Views
{
    /// <summary>
    /// Interaction logic for CryptoInfo.xaml
    /// </summary>
    public partial class CurrencyView : UserControl
    {
        /// <summary>
        /// Constructor of <see cref="CurrencyView"/>
        /// </summary>
        /// <param name="model">Currency Model</param>
        public CurrencyView(CurrencyModel model)
        {
            InitializeComponent();
            DataContext = new CurrencyViewModel(model);
        }
    }
}
