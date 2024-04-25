using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Bankable.Models;
using Bankable.Services;
using Bankable.ViewModels.Dialogs;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
	public static MainWindowViewModel CurrentMainWindowViewModel { get; private set; }
	
	private BankableContext _context = new();

	private readonly UserService _userService = new();
	private readonly TokenService _tokenService = new();
	private readonly AuthenticationService _authenticationService = new();
	private readonly InitializationService _initializationService = new();

	private bool _isAuthenticated;
	private string _currentUsername;

	
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
		// Initialise Singleton for the mainWindow
		CurrentMainWindowViewModel = this;
		
		InitializeMainWindow();
	}
	
	private async void InitializeMainWindow()
	{
		
		// Initialize default data (e.g. Categories) when there is no default data in Database.
		await _initializationService.InitializeData();

		await SetCurrentUser();
		SetContentViewModelAccordingToIsAuth();
	}

	private async Task SetCurrentUser()
	{
		try
		{
			var currentToken = await _tokenService.GetToken();
			BankableContext.CurrentConnectedUser = await _userService.GetItemByID(currentToken.UserId);
			CurrentUsername = BankableContext.CurrentConnectedUser.Username;
			IsAuthenticated = true;
		}
		catch (Exception exception)
		{
			IsAuthenticated = false;
			Console.WriteLine(exception.Message);
		}
	}

	public void SetContentViewModelAccordingToIsAuth()
	{
		if (!IsAuthenticated)
			ContentViewModel = new NotAuthenticatedViewModel();
		else
		{
			ContentViewModel = new HomeViewModel();
			CurrentUsername = BankableContext.CurrentConnectedUser.Username;
		}
	}

	public void LogOut()
	{
		_authenticationService.Logout();
		IsAuthenticated = false;
		SetContentViewModelAccordingToIsAuth();
	}

	private async void ShowAddDialog(ViewModelBase addDialogViewModel)
	{
		if(DialogHost.IsDialogOpen("AddDialog"))
			DialogHost.Close("AddDialog");
		await DialogHost.Show(addDialogViewModel, "AddDialog");
	}

	public bool IsAuthenticated
	{
		get => _isAuthenticated; 
		set => this.RaiseAndSetIfChanged(ref _isAuthenticated, value);
	}

	public string CurrentUsername
	{
		get => _currentUsername; 
		set => this.RaiseAndSetIfChanged(ref _currentUsername, value);
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
