using System;
using CryptocurrencyData.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptocurrencyData.Data
{
	public class CryptoDbContext : DbContext
	{
		public CryptoDbContext(DbContextOptions<CryptoDbContext> options) : base(options)
		{

		}

		public DbSet<CryptoData> CryptoData { get; set; }
	}
}

