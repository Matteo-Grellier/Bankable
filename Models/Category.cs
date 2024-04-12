using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bankable.Models;

public class Category
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid Id { get; set; }

	[MaxLength(50)]
	public required string Name { get; set; }

	public ICollection<Incoming> incomings = [];

	public ICollection<Spending> spendings = [];
}
