using CryptoApp.ViewModels;
using Haley.WPF.Controls;

namespace CryptoApp
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : PlainWindow
    {
        /// <summary>
        /// Constructor of <see cref="StartWindow"/>
        /// </summary>
        public StartWindow()
        {
            InitializeComponent();
            DataContext = new HeaderViewModel();
        }
    }
}
