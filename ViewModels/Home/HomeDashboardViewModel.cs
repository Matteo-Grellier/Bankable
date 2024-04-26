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
        SetLast3spendingsAsync();
        SetReccurentAsync();
    }
    
    private ViewModelBase _donutChartViewModel = new DonutChartViewModel();
    public ViewModelBase DonutChartViewModel
    {
        get => _donutChartViewModel;
        private set => this.RaiseAndSetIfChanged(ref _donutChartViewModel, value);
    }
    
    private ViewModelBase _barsChartViewModel = new BarsChartViewModel();
    public ViewModelBase BarsChartViewModel
    {
        get => _barsChartViewModel;
        private set => this.RaiseAndSetIfChanged(ref _barsChartViewModel, value);
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
    
    public string TotalUsefull 
    {
        get
        {
            List<Spending> allSpendings = _spendingService.GetItemsForUser();
            double total = 0;
            foreach (Spending spending in allSpendings)
            {
                if (spending.IsUseful)
                {
                    total += spending.Amount;
                }
            }
            return total + "€";
        }
    }
    
    public string TotalUseless 
    {
        get
        {
            List<Spending> allSpendings = _spendingService.GetItemsForUser();
            double total = 0;
            foreach (Spending spending in allSpendings)
            {
                if (!spending.IsUseful)
                {
                    total += spending.Amount;
                }
            }
            return total + "€";
        }
    }
    
    public double SavedMonth { get; set; }
    public double SavedYear { get; set; }
    
    private double _reccurent = 0;
    public double Reccurent
    {
        get => _reccurent;
        private set => this.RaiseAndSetIfChanged(ref _reccurent, value);
    }

    private async void SetReccurentAsync()
    {
        List<Spending> allSpendings = await _spendingService.GetAllSpendingsRecurentByMonth(true, DateTime.Now);
        double total = 0;
        foreach (Spending spending in allSpendings)
        {
            total += spending.Amount;
        }
        Reccurent = total;
    }
    
    private List<string> _last3spendings = new() {"none", "none", "none"};
    public List<string> Last3spendings
    {
        get => _last3spendings;
        private set => this.RaiseAndSetIfChanged(ref _last3spendings, value);
    }

    private async void SetLast3spendingsAsync()
    {
        List<Spending> allSpendings = await _spendingService.GetAllInMonth(DateTime.UtcNow);
        List<string> stringList = new();
        if (allSpendings.Count < 3) { throw new Exception("not enough elements in array"); }
        int index = allSpendings.Count - 1;
        stringList.Add(allSpendings[index].Title + " - " + allSpendings[index].Amount + "€");
        stringList.Add(allSpendings[index -1].Title + " - " + allSpendings[index -1].Amount + "€");
        stringList.Add(allSpendings[index -2].Title + " - " + allSpendings[index -2].Amount + "€");
        Last3spendings = stringList;
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