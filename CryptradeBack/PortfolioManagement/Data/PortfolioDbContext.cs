using System;
using PortfolioManagement.Entity;
using Microsoft.EntityFrameworkCore;

namespace PortfolioManagement.Data
{
	public class PortfolioDbContext : DbContext
	{
        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options): base (options)
		{

        }

        public DbSet<Portfolio> Portfolios { get; set; }
    }
}

