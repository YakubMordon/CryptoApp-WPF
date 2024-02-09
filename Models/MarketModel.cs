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
        public string Pair { get; set; }
        public decimal Price { get; set; }
    }
}
