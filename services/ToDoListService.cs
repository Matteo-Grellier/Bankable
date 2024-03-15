using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ToDoList.DataModel;

namespace ToDoList.Services
{
	public class ToDoListService
	{
		
		public IEnumerable<Todo> GetItems() 
		{
			var todo = new TodoContext();
			var todoList = todo.Todos;
			return todoList;
		}

		public void AddItem(Todo todo)
		{
			var test = new Todo() { Description = todo.Description, IsChecked = false };
			var todoContext = new TodoContext();
			todoContext.Add(test);
			todoContext.SaveChanges();
		}
	}
}
