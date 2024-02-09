using CryptoApp.ViewModels;
using Haley.WPF.Controls;

namespace CryptoApp
{
    /// <summary>
    /// Логика взаимодействия для StartWindow.xaml
    /// </summary>
    public partial class StartWindow : PlainWindow
    {
        public StartWindow()
        {
            InitializeComponent();
            DataContext = new HeaderViewModel();
        }
    }
}
