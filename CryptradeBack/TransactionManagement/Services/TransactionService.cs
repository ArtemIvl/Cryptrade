using System;
using Microsoft.EntityFrameworkCore;
using TransactionManagement.Data;
using TransactionManagement.Entity;

namespace TransactionManagement.Services
{
	public class TransactionService
	{

		private readonly TransactionDbContext _context;

		public TransactionService(TransactionDbContext context)
		{
			_context = context;
		}

        public void AddTransaction(Transaction model)
        {
			_context.Transactions.Add(model);
			_context.SaveChanges();
		}

        public void DeleteTransaction(int id)
        {
			var transaction = _context.Transactions.FirstOrDefault(t => t.id == id);
            if (transaction != null)
			{
                _context.Transactions.Remove(transaction);
                _context.SaveChanges();
            }
        }
    }
}

