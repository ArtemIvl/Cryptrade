﻿using System;
namespace CryptocurrencyData.Models
{
	public class CryptoData
	{
		public int id { get; set; }
		public string name { get; set; }
		public string symbol { get; set; }
		public double price { get; set; }
		public double marketCap { get; set; }
		public double volume24h { get; set; }
		public double percentChange24h { get; set; }
        public DateTime lastUpdated { get; set; }
    }
}


