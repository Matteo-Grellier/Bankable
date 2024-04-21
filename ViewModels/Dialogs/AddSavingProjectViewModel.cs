using System;
using System.Reactive;
using Bankable.Models;
using Bankable.Services;
using DialogHostAvalonia;
using ReactiveUI;

namespace Bankable.ViewModels.Dialogs;

public class AddSavingProjectViewModel: ViewModelBase
{
    private SavingProjectService _savingProjectService = new();

    private string _title;
    private DateTimeOffset _willEndAt;
    private float? _finalAmount; // Nullable float due to NumericUpDown from AddIncomingView
    
    public ReactiveCommand<Unit, SavingProject> ConfirmationCommand { get; }

    public AddSavingProjectViewModel()
    {
        // Define default end date to today.
        WillEndAt = new DateTimeOffset(DateTime.Now);
        
        var isValidObservable = this.WhenAnyValue(
            x => x.Title,
            x => x.FinalAmount,
            (title, finalAmount) => !string.IsNullOrEmpty(title) && float.IsPositive(finalAmount ?? -1.0f)
        );
        
        ConfirmationCommand = ReactiveCommand.Create(
            () =>
            {
                try
                {
                    var newSavingProject = _savingProjectService.Add(
                        new SavingProject()
                        {
                            Title = Title, 
                            WillEndAt = WillEndAt.DateTime,
                            FinalAmount = FinalAmount ?? 0,
                            CurrentAmountSaved = 0, 
                            UserId = BankableContext.CurrentConnectedUser.Id
                        }
                    ).Result.Entity;
                    DialogHost.Close("AddDialog");
                    return newSavingProject;
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

    public DateTimeOffset WillEndAt
    {
        get => _willEndAt;
        set => this.RaiseAndSetIfChanged(ref _willEndAt, value);
    }

    public float? FinalAmount     
    {         
        get => _finalAmount; 
        set => this.RaiseAndSetIfChanged(ref _finalAmount, value); 
    }
}