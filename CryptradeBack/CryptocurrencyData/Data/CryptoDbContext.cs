using System;
using CryptocurrencyData.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace CryptocurrencyData.Data
{
	public class CryptoDbContext : DbContext
	{
        //public CryptoDbContext(DbContextOptions<CryptoDbContext> options) : base(options)
        //{
        //    try
        //    {
        //        var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
        //        if (databaseCreator != null)
        //        {
        //            if (!databaseCreator.CanConnect()) databaseCreator.Create();

        //            if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.ToString());
        //    }
        //}

        public CryptoDbContext(DbContextOptions<CryptoDbContext> options) : base(options)
        {

        }

        public DbSet<CryptoData> CryptoData { get; set; }
    }
}