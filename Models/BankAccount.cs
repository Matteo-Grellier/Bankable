using System;
using System.ComponentModel.DataAnnotations;

namespace Bankable.Models;

public class BankAccount
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
    
    [MaxLength(50)] 
    public string Name { get; set; } = null!;
    
    [MaxLength(50)] 
    public string Description { get; set; } = null!;
    
    public float Amount { get; set; }
    public User User { get; set; } = null!;
}