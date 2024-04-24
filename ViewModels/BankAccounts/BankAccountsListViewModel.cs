using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Bankable.Models;
using Bankable.Services;
using Bankable.Views;
using ReactiveUI;

namespace Bankable.ViewModels.BankAccounts;

public class BankAccountsListViewModel: ViewModelBase, IDashboardListViewModel
{
    private readonly SpendingService _spendingService = new();
    private readonly IncomingService _incomingService = new();
    
    private DateTimeOffset _selectedDate;
    private List<Spending> _spendings;
    private List<Incoming> _incomes;

    public BankAccountsListViewModel()
    {
        SelectedDate = new DateTimeOffset(DateTime.Now);
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
    
    public List<Spending> Spendings 
    {
        get => _spendings; 
        set => this.RaiseAndSetIfChanged(ref _spendings, value);
    }
    
    public List<Incoming> Incomes 
    {
        get => _incomes; 
        set => this.RaiseAndSetIfChanged(ref _incomes, value);
    }

    private void OnDateChanged(DateTimeOffset date)
    {
        Console.WriteLine(date);
        SetValues(date);
    }

    private async void SetValues(DateTimeOffset date)
    {
        Spendings = await _spendingService.GetAllInMonth(date.DateTime);
        Incomes = await _incomingService.GetAllInMonth(date.DateTime);
    }
}