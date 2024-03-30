using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using Path = System.IO.Path;

namespace Bankable.Models;

public partial class SavingProjectContext : DbContext
{
    public DbSet<SavingProject> SavingProjects { get; set; }

    public string DbPath { get; }

    public SavingProjectContext()
    {
        // string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "bankable");

        // Directory.CreateDirectory(folder);
        
        DbPath = Path.Join("/home/matteog/Documents/Bankable/Models/", "test.db");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
    
}
public class SavingProject
{
    public Guid Id { get; set; }
    
    public Guid userId { get; set; }

    public DateTime createdAt { get; set; }

    public DateTime updateAt { get; set; }
    
    public DateTime willEndAt { get; set; }
    
    public float finalAmount { get; set; }
    
    public float currentAmountSaved { get; set; }

    public string firstName { get; set; }

    public string lastName { get; set; }
}