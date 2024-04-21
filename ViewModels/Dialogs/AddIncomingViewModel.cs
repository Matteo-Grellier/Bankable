using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using Bankable.Models;
using Bankable.Services;
using DialogHostAvalonia;
using ReactiveUI;

namespace Bankable.ViewModels.Dialogs;

public class AddIncomingViewModel: ViewModelBase
{
    private readonly CategoryService _categoryService = new();
    private readonly BankAccountService _bankAccountService = new();
    private readonly IncomingService _incomingService = new();

    private string _title;
    private string _description;
    private float? _amount; // Nullable float due to NumericUpDown from AddIncomingView
    private DateTimeOffset _date;

    private Category _selectedCategory;
    private BankAccount _selectedBankAccount;
    
    private List<BankAccount> _availableBankAccounts;
    private List<Category> _availableCategories;
    
    public ReactiveCommand<Unit, Incoming> ConfirmationCommand { get; }
    
    public AddIncomingViewModel()
    {
        // Get categories and bank accounts when we instantiate the AddIncomingViewModel
        SetAvailableProperties();
        
        // Define default date to today.
        Date = new DateTimeOffset(DateTime.Today);
        
        var isValidObservable = this.WhenAnyValue(
            x => x.Title,
            x => x.Description,
            x => x.Amount,
            x => x.SelectedBankAccount,
            x => x.SelectedCategory,
            (title, description, amount, selectedBankAccount, selectedCategory) => 
                !string.IsNullOrEmpty(title) 
                && !string.IsNullOrEmpty(description) 
                && float.IsPositive(amount ?? -1.0f)
                && selectedBankAccount != null
                && selectedCategory != null
        );
        
        ConfirmationCommand = ReactiveCommand.Create(
            () =>
            {
                try
                {
                    var newIncome = _incomingService.AddItem(
                        new Incoming
                        {
                            Description = Description, 
                            Title = Title, 
                            Amount = Amount ?? 0,
                            Date = Date.DateTime,
                            BankAccountId = SelectedBankAccount.Id, 
                            CategoryId = SelectedCategory.Id
                        }
                    ).Result.Entity;
                    
                    DialogHost.Close("AddDialog");
                    return newIncome;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

            }, isValidObservable);
    }
    
    public string Title
    {
        get => _title;
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }
    public string Description
    {
        get => _description; 
        set => this.RaiseAndSetIfChanged(ref _description, value);
    }
    public float? Amount
    {
        get => _amount;
        set => this.RaiseAndSetIfChanged(ref _amount, value);
    }
    public DateTimeOffset Date
    {
        get => _date;
        set => this.RaiseAndSetIfChanged(ref _date, value);
    }
    
    public Category SelectedCategory    
    {
        get => _selectedCategory; 
        set => this.RaiseAndSetIfChanged(ref _selectedCategory, value);
    }
    public BankAccount SelectedBankAccount     
    {
        get => _selectedBankAccount; 
        set => this.RaiseAndSetIfChanged(ref _selectedBankAccount, value);
    }

    public List<Category> AvailableCategories
    {
        get => _availableCategories; 
        set => this.RaiseAndSetIfChanged(ref _availableCategories, value);
    }
    public List<BankAccount> AvailableBankAccounts
    {
        get => _availableBankAccounts; 
        set => this.RaiseAndSetIfChanged(ref _availableBankAccounts, value);
    }
    
    private async void SetAvailableProperties()
    {
        AvailableCategories = await _categoryService.GetAllItems();
        AvailableBankAccounts = await _bankAccountService.GetItemsByUser();
    }
}