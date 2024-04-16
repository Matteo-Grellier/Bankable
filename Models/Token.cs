using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bankable.Models;

public class Token
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid Id { get; set; }
	public Guid UserId { get; set; }
	public DateTime CreatedAt
	{ get; set; } = DateTime.UtcNow;

	[MaxLength(50)]
	public Guid Value { get; set; }
	public User User { get; set; }
}
