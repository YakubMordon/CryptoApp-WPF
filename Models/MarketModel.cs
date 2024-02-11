using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Models
{
    /// <summary>
    /// Model for containing Market data
    /// </summary>
    public class MarketModel
    {
        /// <summary>
        /// Exchange identifier, demonstrates on which market was transaction done
        /// </summary>
        public string ExchangeId { get; set; }

        /// <summary>
        /// Currency identifier
        /// </summary>
        public string BaseId { get; set; }

        /// <summary>
        /// Quote identifier, which was used to buy currency
        /// </summary>
        public string QuoteId { get; set; }

        /// <summary>
        /// Price of Currency on market
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// List of <see cref="CandlestickModel"/> for building chart
        /// </summary>
        public List<CandlestickModel> Candles { get; set; }
    }
}
