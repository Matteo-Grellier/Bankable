namespace Bankable.ViewModels.BankAccounts;
using System.Collections.Generic;
using Models;
using Services;
using ReactiveUI;

public class ListIncomesViewModel: ViewModelBase
{

    private readonly IncomingService _incomeService = new();
    private readonly BankAccountService _bankAccountService = new();
    private readonly CategoryService _categoryService = new();

    private IEnumerable<Incoming> _incomes;

    public ListIncomesViewModel()
    {
        GetIncomes();
    }

    public IEnumerable<Incoming> Incomes
    {
        get => _incomes;
        set => this.RaiseAndSetIfChanged(ref _incomes, value);
    }

    private async void GetIncomes()
    {
        Incomes = await _incomeService.GetAllItems();

        foreach(var spending in Incomes)
        {
            spending.BankAccount = await _bankAccountService.GetItemByID(spending.BankAccountId);
            spending.Category = await _categoryService.GetItemByID(spending.CategoryId);
        }

    }
}
