using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reactive;
using Bankable.Models;
using Bankable.Services;
using DialogHostAvalonia;
using ReactiveUI;

namespace Bankable.ViewModels.Dialogs;

public class AddSpendingViewModel: ViewModelBase
{
    private readonly SpendingService _spendingService = new();
    private readonly BankAccountService _bankAccountService = new();
    private readonly CategoryService _categoryService = new();

    private float? _amount; // Nullable float due to NumericUpDown from AddSpendingView
    private DateTimeOffset _date;
    private string _title;
    private string _description;
    private bool _isUseful;
    private bool _isRecurring;
 
    private BankAccount _selectedBankAccount;
    private Category _selectedCategory;

    private List<BankAccount> _availableBankAccounts;
    private List<Category> _availableCategories;
    
    public ReactiveCommand<Unit, Spending> ConfirmationCommand { get; }

    public AddSpendingViewModel()
    {
        // Get categories and bank accounts to set available properties;
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
                    var newSpending = _spendingService.AddItem(
                        new Spending
                        {
                            Description = Description, 
                            Title = Title, 
                            Amount = Amount ?? 0,
                            Date = Date.DateTime,
                            IsUseful = IsUseful,
                            IsRecurrent = IsRecurring,
                            BankAccountId = SelectedBankAccount.Id, 
                            CategoryId = SelectedCategory.Id
                        }
                    ).Result.Entity;
                    DialogHost.Close("AddDialog");
                    return newSpending;
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
    public bool IsUseful
    {
        get => _isUseful;
        set => this.RaiseAndSetIfChanged(ref _isUseful, value);
    }
    public bool IsRecurring
    {
        get => _isRecurring;
        set => this.RaiseAndSetIfChanged(ref _isRecurring, value);
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

    public List<Category> AvailableCategories { get; set; }
    public List<BankAccount> AvailableBankAccounts { get; set; }

    private async void SetAvailableProperties()
    {
        AvailableCategories = await _categoryService.GetAllItems();
        AvailableBankAccounts = await _bankAccountService.GetItemsByUser();
    }
}