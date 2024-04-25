using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform;

namespace Bankable.ViewModels.BankAccounts;
using ReactiveUI;

public class BankAccountsListViewModel: ViewModelBase
{
    private ViewModelBase _contentViewModel;

    public BankAccountsListViewModel()
    {
        ContentViewModel = new ListSpendingsViewModel();
    }

    public ViewModelBase ContentViewModel
    {
        get => _contentViewModel;
        private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
    }

    public void Spendings()
    {
        ContentViewModel = new ListSpendingsViewModel();
    }

    public void Incomes()
    {
        ContentViewModel = new ListIncomesViewModel();
    }
}
