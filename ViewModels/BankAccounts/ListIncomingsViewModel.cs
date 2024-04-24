namespace Bankable.ViewModels.BankAccounts;
using System.Collections.Generic;
using Models;
using Services;
using ReactiveUI;

public class ListIncomingsViewModel: ViewModelBase
{

    private readonly IncomingService _incomingService = new();
    private readonly BankAccountService _bankAccountService = new();
    private readonly CategoryService _categoryService = new();

    private IEnumerable<Incoming> _incomings;

    public ListIncomingsViewModel()
    {
        // Get categories and bank accounts when we instantiate the AddIncomingViewModel
        GetIncomings();
    }

    public IEnumerable<Incoming> Incomings
    {
        get => _incomings;
        set => this.RaiseAndSetIfChanged(ref _incomings, value);
    }

    private async void GetIncomings()
    {
        Incomings = await _incomingService.GetAllItems();

        foreach(var spending in Incomings)
        {
            spending.BankAccount = await _bankAccountService.GetItemByID(spending.BankAccountId);
            spending.Category = await _categoryService.GetItemByID(spending.CategoryId);
        }

    }
}
