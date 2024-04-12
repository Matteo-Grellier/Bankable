using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bankable.Models;

public class User
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid Id { get; set; }

	public DateTime CreatedAt { get; set; } = DateTime.Now;

	public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

	[MaxLength(50)]
	public string FirstName { get; set; } = null!;

	[MaxLength(50)]
	public string LastName { get; set; } = null!;
	public ICollection<SavingProject> SavingProjects { get; set; } = new List<SavingProject>();

	public ICollection<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();

}
