using ReactiveUI;
using System;
using System.Diagnostics;
using System.Reactive;
using System.Threading.Tasks;
using ToDoList.DataModel;
using ToDoList.Services;

namespace ToDoList.ViewModels
{
	public class AddItemViewModel : ViewModelBase
	{
		public ToDoListService _toDoListService;
		
		private string _description;
		private bool _isChecked;

		public ReactiveCommand<Unit, Todo> OkCommand { get; }
		public ReactiveCommand<Unit, Unit> CancelCommand { get; }

		public AddItemViewModel()
		{
			var isValidObservable = this.WhenAnyValue(
				x => x.Description,
				x => !string.IsNullOrWhiteSpace(x));

			OkCommand = ReactiveCommand.Create(
				() => new Todo() { Description = Description }, isValidObservable);
			CancelCommand = ReactiveCommand.Create(() => { });
		}

		public string Description
		{
			get => _description;
			set => this.RaiseAndSetIfChanged(ref _description, value);
		}

		public bool IsChecked
		{
			get => _isChecked;
			set => this.RaiseAndSetIfChanged(ref _isChecked, value);
		}
	}
}
