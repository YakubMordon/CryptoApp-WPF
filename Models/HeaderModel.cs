using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CryptoApp.Models
{
    class HeaderModel
    {
        public UserControl CurrentPage { get; set; }
        public CurrencyModel? CurrentCurrencyModel { get; set; }
        public Visibility PlaceholderVisibility { get; set; }
        public string SearchText { get; set; }
    }
}
