using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Bankable.Models;
using Bankable.Services;
using Bankable.ViewModels.Home;
using Bankable.Views;
using ReactiveUI;

namespace Bankable.ViewModels.BankAccounts;

public class BankAccountsDashboardViewModel: ViewModelBase, IDashboardListViewModel
{
    private readonly SpendingService _spendingService = new();
    private readonly IncomingService _incomingService = new();
    
    private DateTimeOffset _selectedDate;
    private List<Spending> _spendings;
    private List<Incoming> _incomes;

    public BankAccountsDashboardViewModel()
    {
        _selectedDate = new DateTimeOffset(DateTime.Now);
        SetValues(_selectedDate);
    }
    
    
    private ViewModelBase _donutChartViewModel = new DonutChartViewModel();
    public ViewModelBase DonutChartViewModel
    {
        get => _donutChartViewModel;
        private set => this.RaiseAndSetIfChanged(ref _donutChartViewModel, value);
    }
    
    private ViewModelBase _spendingLineViewModel = new SpendingLineChartViewModel();
    public ViewModelBase SpendingLineViewModel
    {
        get => _spendingLineViewModel;
        private set => this.RaiseAndSetIfChanged(ref _spendingLineViewModel, value);
    }
    
    private ViewModelBase _gaugeChartViewModel = new GaugeChartViewModel();
    public ViewModelBase GaugeChartViewModel
    {
        get => _gaugeChartViewModel;
        private set => this.RaiseAndSetIfChanged(ref _gaugeChartViewModel, value);
    }

    private double _totalIncomings = 1700;
    public string TotalIncomings 
    {
        get => _totalIncomings + "€";
    }
    
    private double _totalUsefull = 650;
    public string TotalUsefull 
    {
        get => _totalUsefull + "€";
    }
    
    private double _totalUseless = 128.91;
    public string TotalUseless 
    {
        get => _totalUseless + "€";
    }
    
    private double[] _last3spendings = {13.99, 25.00, 102.75};
    public List<string> Last3spendings 
    {
        get => new List<string>() {"Courses" + " " + _last3spendings[0] + "€", "Boite de nuit" + " " + _last3spendings[1] + "€", "Medecin" + " " + _last3spendings[2] + "€" };
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

    private void OnDateChanged(DateTimeOffset date)
    {
        Console.WriteLine(date);
        SetValues(date);
    }

    private async void SetValues(DateTimeOffset date)
    {
        Spendings = await _spendingService.GetAllInMonth(date.DateTime);
        Incomes = await _incomingService.GetAllInMonth(date.DateTime);
    }
}
