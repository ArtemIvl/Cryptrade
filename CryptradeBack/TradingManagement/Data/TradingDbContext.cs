using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using TradingManagement.Entity;

namespace TradingManagement.Data
{
    public class TradingDbContext : DbContext
    {
        //public TradingDbContext(DbContextOptions<TradingDbContext> options) : base(options)
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

        public TradingDbContext(DbContextOptions<TradingDbContext> options) : base(options)
        {

        }

        public DbSet<Order> Orders { get; set; }
    }
}

