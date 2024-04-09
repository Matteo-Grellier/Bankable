using System;
using System.ComponentModel.DataAnnotations;

namespace Bankable.Models;

public class Saving
{
	public Guid Id { get; set; } = Guid.NewGuid();

	public Guid SavingProjectId { get; set; }

	public DateTime Date { get; set; }

	public float Amount { get; set; }

	public SavingProject SavingProject { get; } = null!;
}
