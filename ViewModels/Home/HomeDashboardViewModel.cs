using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Bankable.Models;
using Bankable.Services;
using Bankable.Views;
using ReactiveUI;

namespace Bankable.ViewModels.Home;

public class HomeDashboardViewModel:ViewModelBase, IDashboardListViewModel
{
    private readonly SpendingService _spendingService = new();
    private readonly IncomingService _incomingService = new();
    private readonly SavingProjectService _savingProjectService = new();
    private readonly SavingService _savingService = new();
    
    private DateTimeOffset _selectedDate;
    private List<Spending> _spendings;
    private List<Incoming> _incomes;
    private List<SavingProject> _savingProjects;
    private List<Saving> _savings;

    public HomeDashboardViewModel()
    {
        _selectedDate = new DateTimeOffset(DateTime.Now);
        SetValues(_selectedDate);
    }
    public DateTimeOffset SelectedDate 
    { 
        get => _selectedDate;
        set
        {
            OnDateChanged(value);
            this.RaiseAndSetIfChanged(ref _selectedDate, value);
        }
    }
    
    public List<Spending> Spendings 
    {
        get => _spendings; 
        set => this.RaiseAndSetIfChanged(ref _spendings, value);
    }
    
    public List<Incoming> Incomes 
    {
        get => _incomes; 
        set => this.RaiseAndSetIfChanged(ref _incomes, value);
    }
    
    public List<SavingProject> SavingProjects 
    {
        get => _savingProjects; 
        set => this.RaiseAndSetIfChanged(ref _savingProjects, value);
    }

    public List<Saving> Savings
    {
        get => _savings; 
        set => this.RaiseAndSetIfChanged(ref _savings, value);
    }

    private void OnDateChanged(DateTimeOffset date)
    {
        Console.WriteLine(date);
        SetValues(date);
    }

    private async void SetValues(DateTimeOffset date)
    {
        Spendings = await _spendingService.GetAllInMonth(date.DateTime);
        Incomes = await _incomingService.GetAllInMonth(date.DateTime);
        SavingProjects = await _savingProjectService.GetItemsForUser();
        Savings = await _savingService.GetAllByUser();
    }
}