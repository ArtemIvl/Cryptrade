using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransactionManagement.Models
{
	public class TransactionDataModel
	{
        public DateTime createdAt { get; set; }

        public double price { get; set; }

        public double amount { get; set; }

        public int id { get; set; }
    }
}

