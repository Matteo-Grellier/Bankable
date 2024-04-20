using System;
using System.Diagnostics;
using System.Linq;
using Bankable.Models;
using Bankable.Services;
using Bankable.ViewModels.Dialogs;
using ReactiveUI;
namespace Bankable.ViewModels;
using DialogHostAvalonia;

public class MainWindowViewModel : ViewModelBase
{
	private ViewModelBase _contentViewModel;

	private BankableContext _context = new();

    public AddBankAccountViewModel AddBankAccountViewModel { get; } = new();
    public AddIncomingViewModel AddIncomingViewModel { get; } = new();
    public AddSavingViewModel AddSavingViewModel { get; } = new();
    public AddSavingProjectViewModel AddSavingProjectViewModel { get; } = new();
    public AddSpendingViewModel AddSpendingViewModel { get; } = new();

	public ViewModelBase ContentViewModel
	{
		get => _contentViewModel;
		private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
	}

    // Change the content of the ContentControl with the corresponding Navbar buttons (Home, BankAccounts, Savings)
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
    
    // Commands
    public async void ShowAddDialog(ViewModelBase addDialogViewModel)
    {
        if(DialogHost.IsDialogOpen("AddDialog"))
            DialogHost.Close("AddDialog");
        await DialogHost.Show(addDialogViewModel, "AddDialog");
    }
}
