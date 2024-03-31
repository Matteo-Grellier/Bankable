using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bankable.ViewModels;

namespace Bankable.Models;

public class Incoming
{
	public Guid Id { get; set; } = Guid.NewGuid();

	public Guid BankAccountId { get; set; }

	public float Amount { get; set; }

	public DateTime Date { get; set; }

	[MaxLength(50)]
	public string Title { get; set; } = null!;

	[MaxLength(50)]
	public string Description { get; set; } = null!;

	public Guid CategoryId { get; set; }

	public BankAccount BankAccount { get; } = null!;

	public Category Category { get; } = null!;

}
