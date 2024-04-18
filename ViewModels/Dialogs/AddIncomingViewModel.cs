using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
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
    
    private CategoryService _categoryService = new();
    private BankAccountService _bankAccountService = new();
    
    public ReactiveCommand<Unit, Incoming> ConfirmationCommand { get; }


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
                DialogHost.Close("AddDialog");
                
                return new Incoming { Description = Description, Title = Title };
            }, isValidObservable);
        // CancelCommand = ReactiveCommand.Create(() => { })
    }
    
    public float Amount
    {
        get => _amount;
        private set => this.RaiseAndSetIfChanged(ref _amount, value);
    }
    public string Title
    {
        get => _title;
        private set => this.RaiseAndSetIfChanged(ref _title, value);
    }
    public string Description
    {
        get => _description; 
        private set => this.RaiseAndSetIfChanged(ref _description, value);
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
    
    public Category SelectedCategory { get; set; }
    public BankAccount SelectedBankAccount { get; set; }
    
    private async void SetAvailableProperties()
    {
        // await _categoryService.AddItem(new Category { Name = "Loisirs" });
        var availableCategories = await _categoryService.GetAllItems();
        AvailableCategories = availableCategories.ToList();

        var availableBankAccounts = await _bankAccountService.GetAllItems();
        AvailableBankAccounts = availableBankAccounts.ToList();
    }
}