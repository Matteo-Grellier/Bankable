namespace Bankable.ViewModels.BankAccounts;
using System.Collections.Generic;
using Models;
using Services;
using ReactiveUI;

public class ListSpendingsViewModel: ViewModelBase
{

    private readonly SpendingService _spendingService = new();
    private readonly BankAccountService _bankAccountService = new();
    private readonly CategoryService _categoryService = new();

    private IEnumerable<Spending> _spendings;

    public ListSpendingsViewModel()
    {
        // Get categories and bank accounts when we instantiate the AddIncomingViewModel
        GetSpendings();
    }

    public IEnumerable<Spending> Spendings
    {
        get => _spendings;
        set => this.RaiseAndSetIfChanged(ref _spendings, value);
    }

    private async void GetSpendings()
    {
        Spendings = await _spendingService.GetAllItems();

        foreach(var spending in Spendings)
        {
            spending.BankAccount = await _bankAccountService.GetItemByID(spending.BankAccountId);
            spending.Category = await _categoryService.GetItemByID(spending.CategoryId);
        }

    }
}
