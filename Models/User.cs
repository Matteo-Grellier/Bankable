using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Path = System.IO.Path;

namespace Bankable.Models;

public class User
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    [MaxLength(50)] 
    public string FirstName { get; set; } = null!;
    
    [MaxLength(50)]
    public string LastName { get; set; } = null!;
    public ICollection<SavingProject> SavingProjects { get; } = new List<SavingProject>();
    
    public ICollection<BankAccount> BankAccounts { get; } = new List<BankAccount>();

}