using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bankable.Models;

public class User
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid Id { get; private set; }

	public DateTime CreatedAt { get; set; } = DateTime.Now;

	public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

	[MaxLength(50)]
	public string FirstName { get; set; } = null!;

	[MaxLength(50)]
	public string LastName { get; set; } = null!;

	[MaxLength(50)]
	public string Username { get; set; } = null!;
	public string Password { get; set; } = null!;

	public Guid TokenId { get; set; }

	public Token? Token { get; } = null!;
	public ICollection<SavingProject> SavingProjects { get; } = new List<SavingProject>();

	public ICollection<BankAccount> BankAccounts { get; } = new List<BankAccount>();
}
