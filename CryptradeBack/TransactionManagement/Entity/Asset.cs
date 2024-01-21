using System;
namespace TransactionManagement.Entity
{
	public class Asset
	{
		public int id { get; set; }
		public string cryptoName { get; set; }
		public string cryptoSymbol { get; set; }
		public double avgBuyPrice { get; set; }
		public double amount { get; set; }
		public int numOfTransactions { get; set; }
		public double percentChange24h { get; set; }
		public double currentPrice { get; set; }
		public double profitLoss { get; set; }
		public int portfolioId { get; set; }
	}
}

