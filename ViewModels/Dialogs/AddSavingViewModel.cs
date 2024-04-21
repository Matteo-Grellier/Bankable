using System;
using System.Collections.Generic;
using System.Reactive;
using Bankable.Models;
using Bankable.Services;
using DialogHostAvalonia;
using ReactiveUI;

namespace Bankable.ViewModels.Dialogs;

public class AddSavingViewModel: ViewModelBase
{
    private SavingService _savingService = new();
    private SavingProjectService _savingProjectService = new();

    private string _name;
    private DateTimeOffset _date;
    private float _amount;
    private SavingProject _selectedSavingProject;

    public ReactiveCommand<Unit, Saving> ConfirmationCommand { get; }

    public AddSavingViewModel()
    {
        // Get saving projects from user and set in available list
        SetAvailableSavingProjects();
        
        // Define default date to today
        Date = new DateTimeOffset(DateTime.Now);
        
        var isValidObservable = this.WhenAnyValue(
            x => x.Name,
            x => x.Amount,
            (name, amount) => !string.IsNullOrEmpty(name) && float.IsPositive(amount)
        );
        
        ConfirmationCommand = ReactiveCommand.Create(
            () =>
            {
                try
                {
                    var newSaving = _savingService.Add(
                        new Saving()
                        {
                            // Name = Name, 
                            Date = Date.DateTime,
                            Amount = Amount,
                            SavingProjectId = SelectedSavingProject.Id
                        }
                    ).Result.Entity;
                    DialogHost.Close("AddDialog");
                    return newSaving;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

            }, isValidObservable);
    }

    private async void SetAvailableSavingProjects()
    {
       AvailableSavingProjects = await _savingProjectService.GetItemsForUser();
    }
    
    public string Name     
    {         
        get => _name; 
        set => this.RaiseAndSetIfChanged(ref _name, value); 
    }

    public DateTimeOffset Date
    {
        get => _date;
        set => this.RaiseAndSetIfChanged(ref _date, value);
    }

    public float Amount     
    {         
        get => _amount; 
        set => this.RaiseAndSetIfChanged(ref _amount, value); 
    }

    public SavingProject SelectedSavingProject
    {
        get => _selectedSavingProject;
        set => this.RaiseAndSetIfChanged(ref _selectedSavingProject, value); 
    }

    public List<SavingProject> AvailableSavingProjects { get; set; }
}