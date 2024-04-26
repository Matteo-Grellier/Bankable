using System;
using System.Reactive;
using Bankable.Models;
using Bankable.Services;
using DialogHostAvalonia;
using ReactiveUI;

namespace Bankable.ViewModels.Dialogs;

public class RegisterViewModel: ViewModelBase
{
    private readonly AuthenticationService _authenticationService = new();

    private string _username;
    private string _firstName;
    private string _lastName;
    private string _password;
    
    public ReactiveCommand<Unit, User> ConfirmationCommand { get; }

    public RegisterViewModel()
    {
        var isValidObservable = this.WhenAnyValue(
            x => x.Username,
            x => x.FirstName,
            x => x.LastName,
            x => x.Password,
            (username, firstName, lastName, password) => 
                !string.IsNullOrEmpty(username) 
                && !string.IsNullOrEmpty(firstName) 
                && !string.IsNullOrEmpty(lastName) 
                && !string.IsNullOrEmpty(password) 
        );
        
        ConfirmationCommand = ReactiveCommand.Create(
            () =>
            {
                try
                {
                    var newUser = _authenticationService.Register(
                        new User
                        {
                            Username = Username, 
                            FirstName = FirstName, 
                            LastName = LastName,
                            Password = Password,
                        }
                    ).Result;
                    
                    DialogHost.Close("UserDialog");
                    return newUser;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

            }, isValidObservable);
    }

    public string Username
    {
        get => _username;
        set => this.RaiseAndSetIfChanged(ref _username, value);
    }

    public string FirstName
    {
        get => _firstName;
        set => this.RaiseAndSetIfChanged(ref _firstName, value);
    }
    
    public string LastName
    {
        get => _lastName;
        set => this.RaiseAndSetIfChanged(ref _lastName, value);
    }

    public string Password
    {
        get => _password;
        set => this.RaiseAndSetIfChanged(ref _password, value);
    }
}