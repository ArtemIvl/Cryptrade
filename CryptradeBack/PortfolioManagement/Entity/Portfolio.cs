using System;
namespace PortfolioManagement.Entity
{
	public class Portfolio
	{
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double totalValue { get; set; }
        public int userId { get; set; }
    }
}

