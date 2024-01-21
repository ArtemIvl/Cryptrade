using System;
namespace PortfolioManagement.Models
{
	public class TotalValueModel
	{
		public double totalValue { get; set; }
		public double profitLoss { get; set; }
		public int portfolioId { get; set; }
		public Performer bestPerformer { get; set; }
		public Performer worstPerformer { get; set; }
	}
}

