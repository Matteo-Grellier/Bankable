using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ToDoList.DataModel;
using ToDoList.Services;

namespace ToDoList.ViewModels
{
	public class ToDoListViewModel : ViewModelBase
	{
		public ToDoListViewModel(IEnumerable<Todo> items)
		{
			ListItems = new ObservableCollection<Todo>(items);
		}

		public ObservableCollection<Todo> ListItems { get; set; }
	}
}
