using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bankable.Models;

public class SavingProject
{
	public Guid Id { get; set; } = Guid.NewGuid();
	public Guid UserId { get; set; }

	[MaxLength(50)]
	public string Title { get; set; } = null!;

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

	public DateTime WillEndAt { get; set; }

	public float FinalAmount { get; set; }

	public float CurrentAmountSaved { get; set; }
	public User User { get; set; } = null!;

	public ICollection<Saving> Savings { get; set; } = new List<Saving>();
}
