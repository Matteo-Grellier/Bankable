using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Bankable.Models;
using Bankable.ViewModels.BankAccounts;
using ReactiveUI;

namespace Bankable.ViewModels;

public class BankAccountsViewModel : ViewModelBase
{
	private ViewModelBase _contentViewModel = null!;

	public ViewModelBase ContentViewModel
	{
		get => _contentViewModel;
		private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
	}

	public async void Dashboard()
	{
		ContentViewModel = new BankAccountsDashboardViewModel();
	}

	ObservableCollection<Spending> Spending { get; set; } = [];
	public void List()
	{
		BankAccountsListViewModel bankAccountsListViewModel = new BankAccountsListViewModel();
		List<Spending> spendings = (List<Spending>)bankAccountsListViewModel.GetItems();
		Spending = new ObservableCollection<Spending>(spendings);
		Console.WriteLine(Spending);
		Console.WriteLine(Spending.Count);
		ContentViewModel = bankAccountsListViewModel;
	}
}
