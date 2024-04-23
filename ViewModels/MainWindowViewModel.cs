using System;
using System.Linq;
using Bankable.Models;
using Bankable.Services;
using ReactiveUI;
namespace Bankable.ViewModels;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Extensions;
using Bankable.Services;
using System.Linq;

public class MainWindowViewModel : ViewModelBase
{
	private UserService userService = new();
	private CategoryService categoryService = new();
	private BankAccountService bankAccountService = new();
	private SpendingService spendingService = new();


	private ViewModelBase _contentViewModel;
	public ViewModelBase ContentViewModel
	{
		get => _contentViewModel;
		private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
	}

	private ViewModelBase _testChartViewModel = new BarsChartViewModel();
	public ViewModelBase TestChartViewModel
	{
		get => _testChartViewModel;
		private set => this.RaiseAndSetIfChanged(ref _testChartViewModel, value);
	}
	DataFaker dataFaker = new();

	public void Home()
	{
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
