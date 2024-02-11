using CryptoApp.ViewModels;
using System.Windows.Controls;

namespace CryptoApp.Views.UserControls
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        /// <summary>
        /// Constructor of <see cref="HomeView"/>
        /// </summary>
        /// <param name="changePage">Method for changing page to Currency Information</param>
        /// <param name="searchText">Text for searching cryptocurrency</param>
        public HomeView(ChangePage changePage, string? searchText)
        {
            InitializeComponent();
            DataContext = new HomeViewModel(changePage, searchText);
        }
    }
}
