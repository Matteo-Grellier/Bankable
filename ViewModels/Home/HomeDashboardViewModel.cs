using System;
using System.Collections.Generic;
using System.Linq;
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

    public HomeDashboardViewModel()
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

    private string _totalUsefull;
    public string TotalUsefull
    {
        get => _totalUsefull;
        set => this.RaiseAndSetIfChanged(ref _totalUsefull, value);
    }

    private string _totalUseless;
    public string TotalUseless
    {
        get => _totalUseless;
        set => this.RaiseAndSetIfChanged(ref _totalUseless, value);
    }

    private string _savedThisMonth;
    public string SavedThisMonth
    {
        get => _savedThisMonth;
        set => this.RaiseAndSetIfChanged(ref _savedThisMonth, value);
    }

    private string _savedThisYear;
    public string SavedThisYear
    {
        get => _savedThisYear;
        set => this.RaiseAndSetIfChanged(ref _savedThisYear, value);
    }

    private string _reccurent;
    public string Reccurent
    {
        get => _reccurent;
        private set => this.RaiseAndSetIfChanged(ref _reccurent, value);
    }

    private List<Spending> _last3spendings;
    public List<Spending> Last3spendings
    {
        get => _last3spendings;
        set => this.RaiseAndSetIfChanged(ref _last3spendings, value);
    }

    private void OnDateChanged(DateTimeOffset date)
    {
        Console.WriteLine(date);
        SetValues(date);
    }

    private async void SetValues(DateTimeOffset date)
    {
        var spendings = await _spendingService.GetAllInMonth(date.DateTime);
        var incomes = await _incomingService.GetAllInMonth(date.DateTime);
        var savingProjects = await _savingProjectService.GetItemsForUser();
        var savings = await _savingService.GetAllByUser();

        // Get the 3 last spendings
        Last3spendings = spendings.OrderByDescending(s => s.Date).Take(3).ToList();

        // get total of usefull and useless spendings
        TotalUsefull = spendings.Where(s => s.IsUseful == true).Sum(s => s.Amount).ToString();
        TotalUseless = spendings.Where(s => s.IsUseful == false).Sum(s => s.Amount).ToString();

        // get total of recurrent spendings
        Reccurent = spendings.Where(s => s.IsRecurring == true).Sum(s => s.Amount).ToString();

        // get all savings from this month
        DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
        SavedThisMonth = savings.Where(
            s => s.Date >= firstDayOfMonth && s.Date <= lastDayOfMonth
        ).Sum(s => s.Amount).ToString();

        DateTime currentDate = DateTime.Now;

        // get all savings from this year
        DateTime firstDayOfYear = new DateTime(currentDate.Year, 1, 1);
        DateTime lastDayOfYear = new DateTime(currentDate.Year, 12, 31);

        var savingsThisYear = savings.Where(s => s.Date >= firstDayOfYear && s.Date <= lastDayOfYear).ToList();
        SavedThisYear = savingsThisYear.Sum(s => s.Amount).ToString();
    }
}
