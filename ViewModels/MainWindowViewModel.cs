using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using ToDoList.DataModel;
using ToDoList.Services;
using System.Reactive.Concurrency;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace ToDoList.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		private ViewModelBase _contentViewModel;
		private ToDoListService toDoListService = new();

		//this has a dependency on the ToDoListService
		public ViewModelBase ContentViewModel
		{
			get => _contentViewModel;
			private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
		}

		public MainWindowViewModel(TodoContext todoContext)
		{
			var service = new ToDoListService();
			ToDoList = new ToDoListViewModel(service.GetItems());
			_contentViewModel = ToDoList;
		}
		public ToDoListViewModel ToDoList { get; set; }

		public void AddItem()
		{
			AddItemViewModel addItemViewModel = new();

			Observable.Merge(
					addItemViewModel.OkCommand,
					addItemViewModel.CancelCommand.Select(_ => (Todo?)null))
				.Take(1)
				.Subscribe(newItem =>
				{
					if (newItem != null)
					{
						var toDoListService = new ToDoListService( );
						toDoListService.AddItem(newItem);
						ToDoList = new ToDoListViewModel(toDoListService.GetItems());
					}
					ContentViewModel = ToDoList;
				});

			ContentViewModel = addItemViewModel;
		}

	}
}
