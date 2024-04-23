using System;
using System.Diagnostics;
using System.Linq;
using Bankable.Models;
using Bankable.Services;
using Bankable.ViewModels.Dialogs;
using ReactiveUI;
using DialogHostAvalonia;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Extensions;

namespace Bankable.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
	private BankableContext _context = new();
	private UserService _userService = new();
	private TokenService _tokenService = new();

	
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

	public MainWindowViewModel()
	{
		SetCurrentUser();
	}

	private async void SetCurrentUser()
	{
		try
		{
			var currentToken = await _tokenService.GetToken();
			BankableContext.CurrentConnectedUser = await _userService.GetItemByToken(currentToken.Id);
		}
		catch (Exception exception)
		{
			Console.WriteLine(exception.Message);
		}

	}
	
	private async void ShowAddDialog(ViewModelBase addDialogViewModel)
	{
		if(DialogHost.IsDialogOpen("AddDialog"))
			DialogHost.Close("AddDialog");
		await DialogHost.Show(addDialogViewModel, "AddDialog");
	}

    // Change the content of the ContentControl with the corresponding Navbar buttons (Home, BankAccounts, Savings)
	DataFaker _dataFaker = new();
	public void Home()
	{
		//_dataFaker.GenerateData();
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
    
    // Commands to show add dialogs according to view models
    public void ShowAddBankAccountViewModel()
    {
	    ShowAddDialog(new AddBankAccountViewModel());
    }

    public void ShowAddIncomingViewModel()
    {
	    ShowAddDialog(new AddIncomingViewModel());
    }
    
    public void ShowAddSavingProjectViewModel()
    {
	    ShowAddDialog(new AddSavingProjectViewModel());
    }
    
    public void ShowAddSavingViewModel()
    {
	    ShowAddDialog(new AddSavingViewModel());
    }
    
    public void ShowAddSpendingViewModel()
    {
	    ShowAddDialog(new AddSpendingViewModel());
    }
    
}
