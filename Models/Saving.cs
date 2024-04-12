using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bankable.Models;

public class Saving
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid Id { get; set; }

	public Guid SavingProjectId { get; set; }

	public DateTime Date { get; set; }

	public float Amount { get; set; }

	public SavingProject SavingProject { get; } = null!;
}
