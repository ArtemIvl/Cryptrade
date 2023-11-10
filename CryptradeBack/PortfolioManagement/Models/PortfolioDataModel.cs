using System;
using System.ComponentModel.DataAnnotations;

namespace PortfolioManagement.Models
{
	public class PortfolioDataModel
	{
		[Required]
		public string name { get; set; }
		public string description { get; set; }
		public double totalValue { get; set; }
	}
}

