using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransactionManagement.Entity
{
	[Table("Transactions")]
	public class Transaction
	{
		[Key]
		[Column("id")]
		public int id { get; set; }

        [Column("createdAt")]
        public DateTime createdAt { get; set; }

        [Column("cryptoName")]
        public string cryptoName { get; set; }

        [Column("cryptoSymbol")]
        public string cryptoSymbol { get; set; }

        [Column("type")]
        public string type { get; set; }

        [Column("buyPrice")]
        public double buyPrice { get; set; }

        [Column("buyAmount")]
        public double buyAmount { get; set; }

        [Column("sellPrice")]
        public double sellPrice { get; set; }

        [Column("sellAmount")]
        public double sellAmount { get; set; }

        [Column("portfolioId")]
        public int portfolioId { get; set; }
	}
}

