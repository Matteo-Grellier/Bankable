using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bankable.Models;

public class Category
{
	public Guid Id { get; set; } = Guid.NewGuid();

	[MaxLength(50)]
	public string Name { get; set; } = null!;

	public ICollection<Incoming> incomings = [];

	public ICollection<Spending> spendings = [];
}
