using System.IO;
using Microsoft.EntityFrameworkCore;

namespace Bankable.Models;

public class BankableContext : DbContext
{
	public DbSet<SavingProject> SavingProjects { get; set; }
	public DbSet<BankAccount> BankAccounts { get; set; }
	public DbSet<Token> Tokens { get; set; }
	public DbSet<Saving> Savings { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<Incoming> Incomings { get; set; }
	public DbSet<Spending> Spendings { get; set; }
	public DbSet<Category> Categories { get; set; }

	public static User CurrentConnectedUser { get; set; } = null!;

	public string DbPath { get; }

	public BankableContext()
	{
		// string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "bankable");

		// Directory.CreateDirectory(folder);

		DbPath = Path.Join("/home/matheoleger/Documents/Github/Ynov/M1/Bankable/Models/", "database.db");
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
			.HasMany(e => e.Savings)
			.WithOne(e => e.SavingProject)
			.HasForeignKey(e => e.SavingProjectId);
		modelBuilder.Entity<BankAccount>()
			.HasMany(e => e.Incomings)
			.WithOne(e => e.BankAccount)
			.HasForeignKey(e => e.BankAccountId);
		modelBuilder.Entity<BankAccount>()
			.HasMany(e => e.Spendings)
			.WithOne(e => e.BankAccount)
			.HasForeignKey(e => e.BankAccountId);
		modelBuilder.Entity<Category>()
			.HasMany(e => e.incomings)
			.WithOne(e => e.Category)
			.HasForeignKey(e => e.CategoryId);
		modelBuilder.Entity<Category>()
			.HasMany(e => e.spendings)
			.WithOne(e => e.Category)
			.HasForeignKey(e => e.CategoryId);
		modelBuilder.Entity<User>()
			.HasOne(e => e.Token)
			.WithOne(e => e.User)
			.HasForeignKey<Token>(e => e.UserId);
	}
	protected override void OnConfiguring(DbContextOptionsBuilder options)
		=> options.UseSqlite($"Data Source={DbPath}");
}
