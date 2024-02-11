using CryptoApp.Models;
using System.ComponentModel;

namespace CryptoApp.ViewModels
{
    /// <summary>
    /// View model for the Currency view.
    /// </summary>
    public class CurrencyViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Current <see cref="CurrencyModel"/>
        /// </summary>
        private CurrencyModel _currency;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyViewModel"/> class.
        /// </summary>
        /// <param name="model"><see cref="CurrencyModel"/> to show.</param>
        public CurrencyViewModel(CurrencyModel model)
        {
            Currency = model;
        }

        #region Properties

        /// <summary>
        /// Gets or sets Current <see cref="CurrencyModel"/>.
        /// </summary>
        public CurrencyModel Currency
        {
            get => _currency;
            set
            {
                _currency = value;
                OnPropertyChanged(nameof(Currency));
            }
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
