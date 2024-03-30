using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bankable.Models;

public class SavingProject
{
	public Guid Id { get; set; }
	public Guid UserId { get; set; }

	[MaxLength(50)]
	public string Title { get; set; } = null!;

	public DateTime CreatedAt { get; set; }

	public DateTime UpdatedAt { get; set; }

	public DateTime WillEndAt { get; set; }

	public float FinalAmount { get; set; }

	public float CurrentAmountSaved { get; set; }
	public User User { get; set; } = null!;

	public ICollection<SparedSpending> SparedSpendings { get; } = new List<SparedSpending>();
}
