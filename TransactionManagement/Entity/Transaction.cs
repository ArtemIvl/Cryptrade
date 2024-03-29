﻿using System;
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

        [Column("price")]
        public double price { get; set; }

        [Column("amount")]
        public double amount { get; set; }

        [Column("portfolioId")]
        public int portfolioId { get; set; }
	}
}

