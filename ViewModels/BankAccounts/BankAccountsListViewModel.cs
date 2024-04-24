using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform;

namespace Bankable.ViewModels.BankAccounts;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Bankable.Models;
using Bankable.Services;
using ReactiveUI;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;

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

    public void Incomings()
    {
        ContentViewModel = new ListIncomingsViewModel();
    }
}
