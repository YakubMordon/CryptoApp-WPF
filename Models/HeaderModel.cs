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
    /// <summary>
    /// Model for handling actions in Header
    /// </summary>
    class HeaderModel
    {
        /// <summary>
        /// Current Page under header
        /// </summary>
        public UserControl CurrentPage { get; set; }

        /// <summary>
        /// Current <see cref="CurrencyModel"/>
        /// </summary>
        public CurrencyModel? CurrentCurrencyModel { get; set; }

        /// <summary>
        /// <see cref="Visibility"/> of search bar input
        /// </summary>
        public Visibility PlaceholderVisibility { get; set; }

        /// <summary>
        /// Search bar input text
        /// </summary>
        public string SearchText { get; set; }
    }
}
