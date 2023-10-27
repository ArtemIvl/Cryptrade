using System;
using System.Text.Json.Serialization;

namespace CryptocurrencyData.Entity
{
	public class Cryptocurrency
	{
		public int id { get; set; }
		public string name { get; set; }
		public string symbol { get; set; }
		public string slug { get; set; }
		public int cmc_rank { get; set; }
        public double circulating_supply { get; set; }
        public double total_supply { get; set; }
        public double? max_supply { get; set; }
        public bool infinite_supply { get; set; }
        public string last_updated { get; set; }
        public string date_added { get; set; }
        public Quote quote { get; set; }
    }

    public class Quote
    {
        public USD usd { get; set; }
    }

    public class USD
    {
        public double price { get; set; }
        public double volume_24h { get; set; }
        public double volume_change_24h { get; set; }
        public double percent_change_24h { get; set; }
        public double market_cap { get; set; }
    }

    public class CryptoApiResponse
    {
        public List<Cryptocurrency> Data { get; set; }
    }
}

