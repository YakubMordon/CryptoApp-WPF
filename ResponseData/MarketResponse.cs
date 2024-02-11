﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.ResponseData
{
    public class MarketResponse
    {
        public List<MarketDataResponse> Data { get; set; }
    }

    public class MarketDataResponse
    {
        public string ExchangeId { get; set; }
        public string BaseId { get; set; }
        public string QuoteId { get; set; }
        public string? BaseSymbol { get; set; }
        public string? QuoteSymbol { get; set; }
        public decimal? VolumeUsd24Hr { get; set; }
        public decimal PriceUsd { get; set; }
        public decimal? VolumePercent { get; set; }
    }
}
