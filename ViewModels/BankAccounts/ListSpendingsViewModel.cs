using System;

namespace Bankable.ViewModels.BankAccounts;
using System.Collections.Generic;
using Models;
using Services;
using ReactiveUI;

public class ListSpendingsViewModel: ViewModelBase, IListVIewModel
{

    private readonly SpendingService _spendingService = new();
    private readonly BankAccountService _bankAccountService = new();
    private readonly CategoryService _categoryService = new();

    private IEnumerable<Spending> _spendings;
    private DateTimeOffset _selectedDate;

    public ListSpendingsViewModel(DateTimeOffset selectedDate)
    {
        // Set the SelectedDate to filter by month
        SelectedDate = selectedDate;
        
        // Get categories and bank accounts when we instantiate the AddIncomingViewModel
        GetSpendings(selectedDate);
    }

    public IEnumerable<Spending> Spendings
    {
        get => _spendings;
        set => this.RaiseAndSetIfChanged(ref _spendings, value);
    }
    
    public DateTimeOffset SelectedDate
    {
        get => _selectedDate;
        set
        {
            GetSpendings(value);
            this.RaiseAndSetIfChanged(ref _selectedDate, value);
        }
    }

    private async void GetSpendings(DateTimeOffset selectedDate)
    {
        // Get Spendings for one month and for the current user
        Spendings = await _spendingService.GetAllInMonth(selectedDate.DateTime);

        foreach(var spending in Spendings)
        {
            spending.BankAccount = await _bankAccountService.GetItemByID(spending.BankAccountId);
            spending.Category = await _categoryService.GetItemByID(spending.CategoryId);
        }
    }
}
