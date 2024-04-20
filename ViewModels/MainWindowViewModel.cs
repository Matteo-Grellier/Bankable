using System;
using System.Linq;
using Bankable.Models;
using Bankable.Services;
using ReactiveUI;
namespace Bankable.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
	private ViewModelBase _contentViewModel;

	private BankableContext _context = new();


	public ViewModelBase ContentViewModel
	{
		get => _contentViewModel;
		private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
	}
	DataFaker dataFaker = new();
	public void Home()
	{
		dataFaker.GenerateData();
		ContentViewModel = new HomeViewModel();
	}

	public void BankAccounts()
	{
		ContentViewModel = new BankAccountsViewModel();
	}

	public void Savings()
	{
		ContentViewModel = new SavingsViewModel();
	}
}
