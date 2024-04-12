using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Bankable.Models;
using Bankable.Services;

namespace Bankable.ViewModels.BankAccounts;

public class BankAccountsListViewModel : ViewModelBase
{
	public ObservableCollection<Spending> People { get; }

	private SpendingService spendingService = new();

	public BankAccountsListViewModel()
	{
	}

	public IEnumerable<Spending> GetItems()
	{
		IEnumerable<Spending> spendings = spendingService.GetAllItems().Result;
		return spendings;
	}

}

