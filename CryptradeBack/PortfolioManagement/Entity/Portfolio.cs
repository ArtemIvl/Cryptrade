using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioManagement.Entity
{
    [Table("Portfolios")]
	public class Portfolio
	{
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Column("name")]
        public string name { get; set; }

        [Column("description")]
        public string description { get; set; }

        [Column("totalValue")]
        public double totalValue { get; set; }

        [Column("userId")]
        public int userId { get; set; }
    }
}

