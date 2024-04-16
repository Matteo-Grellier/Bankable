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

    private UserService _userService = new();

    public MainWindowViewModel()
    {
        // if (_userService.GetAllItems() == null)
        // {
        //     var newUser = new User{FirstName = "Mathéo", LastName = "LEGER"};
        //     _userService.AddItem(newUser);
        //
        // }
        
        // UserService.CurrentUser = _userService.GetAllItems().Result.First();
        
        Console.WriteLine(UserService.CurrentUser);
        
        // var newUser = new User{FirstName = "Mathéo", LastName = "LEGER"};
        // _userService.AddItem(newUser);
    }

    public ViewModelBase ContentViewModel
    {
        get => _contentViewModel;
        private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
    }

    public AddBankAccountViewModel AddBankAccountViewModel { get; } = new();
    public AddIncomingViewModel AddIncomingViewModel { get; } = new();
    public AddSavingViewModel AddSavingViewModel { get; } = new();
    public AddSavingProjectViewModel AddSavingProjectViewModel { get; } = new();
    public AddSpendingViewModel AddSpendingViewModel { get; } = new();

    // Change the content of the ContentControl with the corresponding Navbar buttons (Home, BankAccounts, Savings)
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
    
    // Commands

    public async void ShowAddDialog(ViewModelBase addDialogViewModel)
    {
        if(DialogHost.IsDialogOpen("AddDialog"))
            DialogHost.Close("AddDialog");
        await DialogHost.Show(addDialogViewModel, "AddDialog");
    }
}
