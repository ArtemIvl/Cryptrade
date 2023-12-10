using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptocurrencyData.Entity
{
    [Table("CryptoData")]
	public class CryptoData
	{
		[Key]
		[Column("id")]
		public int id { get; set; }

        [Column("name")]
        public string name { get; set; }

        [Column("symbol")]
        public string symbol { get; set; }
        
        [Column("price")]
        public double price { get; set; }

        [Column("marketCap")]
        public double marketCap { get; set; }

        [Column("volume24h")]
        public double volume24h { get; set; }

        [Column("percentChange24h")]
        public double percentChange24h { get; set; }

        [Column("circulatingSupply")]
        public double? circulatingSupply { get; set; }

        [Column("cmcRank")]
        public int? cmcRank { get; set; }

        [Column("lastUpdated")]
        public DateTime lastUpdated { get; set; }
    }
}