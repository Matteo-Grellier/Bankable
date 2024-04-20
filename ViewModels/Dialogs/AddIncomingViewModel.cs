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

    private string _title;
    private string _description;
    private float _amount;
    private List<BankAccount> _avalaibleBankAccounts;
    private List<Category> _availableCategories;

    private Category _selectedCategory;
    private BankAccount _selectedBankAccount;
    
    private CategoryService _categoryService = new();
    private BankAccountService _bankAccountService = new();
    private IncomingService _incomingService = new();
    
    public ReactiveCommand<Unit, Incoming> ConfirmationCommand { get; }
    
    public float Amount
    {
        get => _amount;
        set => this.RaiseAndSetIfChanged(ref _amount, value);
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

    public List<Category> AvailableCategories
    {
        get => _availableCategories; 
        set => this.RaiseAndSetIfChanged(ref _availableCategories, value);
    }

    public List<BankAccount> AvailableBankAccounts
    {
        get => _avalaibleBankAccounts; 
        set => this.RaiseAndSetIfChanged(ref _avalaibleBankAccounts, value);
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
    
    public AddIncomingViewModel()
    {
        // Get categories and bank accounts when we instantiate the AddIncomingViewModel
        SetAvailableProperties();
        
        var isValidObservable = this.WhenAnyValue(
            x => x.Title,
            x => x.Description,
            x => x.Amount,
            (title, description, amount) => !string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(description)
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
                            Amount = Amount,
                            BankAccountId = SelectedBankAccount.Id, 
                            CategoryId = SelectedCategory.Id,
                            Date = new DateTime()
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
        // CancelCommand = ReactiveCommand.Create(() => { })
    }
    
    private async void SetAvailableProperties()
    {
        AvailableCategories = await _categoryService.GetAllItems();
        AvailableBankAccounts = await _bankAccountService.GetItemsByUser();
    }
}