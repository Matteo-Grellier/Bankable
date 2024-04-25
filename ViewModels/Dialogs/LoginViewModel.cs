using System;
using System.Reactive;
using Bankable.Models;
using Bankable.Services;
using DialogHostAvalonia;
using ReactiveUI;

namespace Bankable.ViewModels.Dialogs;

public class LoginViewModel: ViewModelBase
{
    private readonly AuthenticationService _authenticationService = new();
    private readonly TokenService _tokenService = new();

    private string _username;
    private string _password;
    
    public ReactiveCommand<Unit, User> ConfirmationCommand { get; }

    public LoginViewModel()
    {
        var isValidObservable = this.WhenAnyValue(
            x => x.Username,
            x => x.Password,
            (username, password) => 
                !string.IsNullOrEmpty(username) 
                && !string.IsNullOrEmpty(password) 
        );
        
        ConfirmationCommand = ReactiveCommand.Create(
            () =>
            {
                try
                {
                    var currentUser = _authenticationService.Login(Username, Password).Result;
                    MainWindowViewModel.CurrentMainWindowViewModel.IsAuthenticated = true;
                    _ = MainWindowViewModel.CurrentMainWindowViewModel.SetContentViewModelAccordingToIsAuth();
                    return currentUser;
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

    public string Password
    {
        get => _password;
        set => this.RaiseAndSetIfChanged(ref _password, value);
    }
}