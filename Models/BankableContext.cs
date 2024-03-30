using System.IO;
using Microsoft.EntityFrameworkCore;

namespace Bankable.Models;

public class BankableContext : DbContext
{
	public DbSet<SavingProject> SavingProjects { get; set; }

	public DbSet<BankAccount> BankAccounts { get; set; }

	public DbSet<SparedSpending> SparedSpendings { get; set; }

	public DbSet<User> Users { get; set; }
	public string DbPath { get; }

	public BankableContext()
	{
		// string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "bankable");

		// Directory.CreateDirectory(folder);

		DbPath = Path.Join("/home/matteog/Documents/Bankable/Models/", "database.db");
	}
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<User>()
			.HasMany(e => e.SavingProjects)
			.WithOne(e => e.User)
			.HasForeignKey(e => e.UserId);
		modelBuilder.Entity<User>()
			.HasMany(e => e.BankAccounts)
			.WithOne(e => e.User)
			.HasForeignKey(e => e.UserId);
		modelBuilder.Entity<SavingProject>()
			.HasMany(e => e.SparedSpendings)
			.WithOne(e => e.SavingProject)
			.HasForeignKey(e => e.SavingProjectId);
	}


	protected override void OnConfiguring(DbContextOptionsBuilder options)
		=> options.UseSqlite($"Data Source={DbPath}");
}
