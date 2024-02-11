using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Models
{
    /// <summary>
    /// Model for displaying Japanese Candlestick Chart
    /// </summary>
    public class CandlestickModel
    {
        /// <summary>
        /// Opening price of the candlestick.
        /// </summary>
        public double Open { get; set; }

        /// <summary>
        /// Highest price reached during the candlestick period.
        /// </summary>
        public double High { get; set; }

        /// <summary>
        /// Lowest price reached during the candlestick period.
        /// </summary>
        public double Low { get; set; }

        /// <summary>
        /// Closing price of the candlestick.
        /// </summary>
        public double Close { get; set; }

        /// <summary>
        /// Trading volume during the candlestick period.
        /// </summary>
        public double Volume { get; set; }

        /// <summary>
        /// Period of the candlestick.
        /// </summary>
        public long Period { get; set; }
    }
}
