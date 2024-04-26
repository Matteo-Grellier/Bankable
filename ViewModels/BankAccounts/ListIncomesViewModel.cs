using System;

namespace Bankable.ViewModels.BankAccounts;
using System.Collections.Generic;
using Models;
using Services;
using ReactiveUI;

public class ListIncomesViewModel: ViewModelBase, IListVIewModel
{

    private readonly IncomingService _incomeService = new();
    private readonly BankAccountService _bankAccountService = new();
    private readonly CategoryService _categoryService = new();

    private IEnumerable<Incoming> _incomes;
    private DateTimeOffset _selectedDate;

    public ListIncomesViewModel(DateTimeOffset selectedDate)
    {
        SelectedDate = selectedDate;
        
        GetIncomes(selectedDate);
    }

    public IEnumerable<Incoming> Incomes
    {
        get => _incomes;
        set => this.RaiseAndSetIfChanged(ref _incomes, value);
    }
    
    public DateTimeOffset SelectedDate
    {
        get => _selectedDate;
        set
        {
            GetIncomes(value);
            this.RaiseAndSetIfChanged(ref _selectedDate, value);
        }
    }

    private async void GetIncomes(DateTimeOffset selectedDate)
    {
        // Get Incomes for one month and for the current user
        Incomes = await _incomeService.GetAllInMonth(selectedDate.DateTime);

        foreach(var spending in Incomes)
        {
            spending.BankAccount = await _bankAccountService.GetItemByID(spending.BankAccountId);
            spending.Category = await _categoryService.GetItemByID(spending.CategoryId);
        }
    }
}
