using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using Path = System.IO.Path;

namespace Bankable.Models;

public partial class UserContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public string DbPath { get; }

    public UserContext()
    {
        // string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "bankable");

        // Directory.CreateDirectory(folder);
        
        DbPath = Path.Join("/home/matteog/Documents/Bankable/Models/", "test.db");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
         => options.UseSqlite($"Data Source={DbPath}");
    
}
public class User
{
    public Guid Id { get; set; }

    public DateTime createdAt { get; set; }

    public DateTime updateAt { get; set; }

    public string firstName { get; set; }

    public string lastName { get; set; }
}