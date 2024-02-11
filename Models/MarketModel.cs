using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Models
{
    public class MarketModel
    {
        public string ExchangeId { get; set; }
        public string BaseId { get; set; }
        public string QuoteId { get; set; }
        public decimal Price { get; set; }
        public List<CandlestickModel> Candles { get; set; }

        public override string ToString()
        {
            return $"{ExchangeId}\t{Price}";
        }
    }
}
