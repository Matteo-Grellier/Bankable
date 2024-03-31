using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bankable.Models;

public class User
{
	public Guid Id { get; set; } = Guid.NewGuid();

	public DateTime CreatedAt { get; set; } = DateTime.Now;

	public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

	[MaxLength(50)]
	public string FirstName { get; set; } = null!;

	[MaxLength(50)]
	public string LastName { get; set; } = null!;
	public ICollection<SavingProject> SavingProjects { get; set; } = new List<SavingProject>();

	public ICollection<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();

}
