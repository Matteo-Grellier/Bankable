using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ToDoList.DataModel
{
	public class TodoContext : DbContext
	{
		public DbSet<Todo> Todos { get; set; }
		private string DbPath { get; }
		
		public TodoContext()
		{
			DbPath = System.IO.Path.Join("/home/matteog/Documents/ToDoList/Models/", "test.db");
		}
		protected override void OnConfiguring(DbContextOptionsBuilder options)
			=> options.UseSqlite($"Data Source={DbPath}");
	}
	
	public class Todo
	{
		public int TodoId { get; set; }
		public string Description { get; set; } = String.Empty;
		public bool IsChecked { get; set; }
	}
}
