using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradingManagement.Entity
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Column("cryptoName")]
        public string cryptoName { get; set; }

        [Column("cryptoSymbol")]
        public string cryptoSymbol { get; set; }

        [Column("userId")]
        public int userId { get; set; }

        [Column("openPrice")]
        public double openPrice { get; set; }

        [Column("closePrice")]
        public double closePrice { get; set; }

        [Column("type")]
        public string type { get; set; }

        [Column("amount")]
        public double amount { get; set; }

        [Column("isOpen")]
        public bool isOpen { get; set; }

        [Column("Finished")]
        public bool finished { get; set; }
    }
}

