using System;
using System.Reactive;
using System.Reactive.Linq;
using Bankable.Models;
using Bankable.Services;
using DialogHostAvalonia;
using ReactiveUI;

namespace Bankable.ViewModels.Dialogs;

public class AddBankAccountViewModel: ViewModelBase
{
    private string _name = string.Empty;
    private string _description = string.Empty;

    private BankAccountService _bankAccountService = new();
    
    public ReactiveCommand<Unit, BankAccount> ConfirmationCommand { get; }

    public AddBankAccountViewModel()
    {
        var isValidObservable = this.WhenAnyValue(
            x => x.Name,
            x => x.Description,
            (name, description) => !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(description)
        );
        
        ConfirmationCommand = ReactiveCommand.Create(
            () =>
            {
                var newBankAccount = new BankAccount { Description = Description, Name = Name };
                _ = _bankAccountService.AddItem(new BankAccount { Description = Description, Name = Name });
                DialogHost.Close("AddDialog");
                return newBankAccount;
            }, isValidObservable);
        // CancelCommand = ReactiveCommand.Create(() => { })
    }
    
    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }
    
    public string Description
    {
        get => _description;
        set => this.RaiseAndSetIfChanged(ref _description, value);
    }
}