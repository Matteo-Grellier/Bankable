using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform;
using Bankable.Services;

namespace Bankable.ViewModels.BankAccounts;
using ReactiveUI;

public class BankAccountsListViewModel: ViewModelBase, IDashboardListViewModel
{
    private readonly SpendingService _spendingService = new();
    private readonly IncomingService _incomingService = new();
    
    private IListVIewModel _contentViewModel;
    
    private DateTimeOffset _selectedDate;


    public BankAccountsListViewModel()
    {
        SelectedDate = DateTime.Now;
        ContentViewModel = new ListSpendingsViewModel(DateTime.Now);
    }
    
    public DateTimeOffset SelectedDate 
    { 
        get => _selectedDate;
        set
        {
            OnDateChanged(value);
            this.RaiseAndSetIfChanged(ref _selectedDate, value);
        }
    }

    public IListVIewModel ContentViewModel
    {
        get => _contentViewModel;
        private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
    }
    
    private void OnDateChanged(DateTimeOffset date)
    {
        Console.WriteLine(date);

        if (ContentViewModel != null) ContentViewModel.SelectedDate = date;
    }
    
    public void Spendings()
    {
        ContentViewModel = new ListSpendingsViewModel(SelectedDate);
    }

    public void Incomes()
    {
        ContentViewModel = new ListIncomesViewModel(SelectedDate);
    }
}
