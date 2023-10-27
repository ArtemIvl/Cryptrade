using System;
using Microsoft.EntityFrameworkCore;
using UserManagement.Entity;

namespace UserManagement.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base (options)
		{

		}

		public DbSet<User> Users { get; set; }
	}
}

