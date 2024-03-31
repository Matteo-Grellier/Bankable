using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bankable.Models;

public class BankAccount
{
	public Guid Id { get; set; } = Guid.NewGuid();
	public Guid UserId { get; set; }

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

	[MaxLength(50)]
	public string Name { get; set; } = null!;

	[MaxLength(50)]
	public string Description { get; set; } = null!;

	public float Amount { get; set; }

	public User User { get; set; } = null!;

	public ICollection<Incoming> Incomings { get; set; } = new List<Incoming>();
	public ICollection<Spending> Spendings { get; set; } = new List<Spending>();

}
