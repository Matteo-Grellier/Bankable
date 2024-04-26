using System;
using System.Collections.Generic;
using System.Linq;
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
    private readonly BankAccountService _bankAccountService = new();

    private DateTimeOffset _selectedDate;

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

    private string _totalIncomes;
    public string TotalIncomes
    {
        get => _totalIncomes;
        set => this.RaiseAndSetIfChanged(ref _totalIncomes, value);
    }

    private string _totalSpendings;
    public string TotalSpendings
    {
        get => _totalSpendings;
        set => this.RaiseAndSetIfChanged(ref _totalSpendings, value);
    }

    private List<Spending> _last3spendings;
    public List<Spending> Last3spendings
    {
        get => _last3spendings;
        set => this.RaiseAndSetIfChanged(ref _last3spendings, value);
    }

    private string _accountBalance;
    public string AccountBalance
    {
        get => _accountBalance;
        private set => this.RaiseAndSetIfChanged(ref _accountBalance, value);
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

    private void OnDateChanged(DateTimeOffset date)
    {
        Console.WriteLine(date);
        SetValues(date);
    }

    private async void SetValues(DateTimeOffset date)
    {
        var spendings = await _spendingService.GetAllInMonth(date.DateTime);
        var incomes = await _incomingService.GetAllInMonth(date.DateTime);
        var bankAccounts = await _bankAccountService.GetAllItems();
        // get total of spendings and incomes
        TotalIncomes = incomes.Sum(i => i.Amount).ToString();
        TotalSpendings = spendings.Sum(s => s.Amount).ToString();

        AccountBalance = bankAccounts.Sum(a => a.Amount).ToString();

        // Get the 3 last spendings
        Last3spendings = spendings.OrderByDescending(s => s.Date).Take(3).ToList();

        // get total of usefull and useless spendings
        TotalUsefull = spendings.Where(s => s.IsUseful == true).Sum(s => s.Amount).ToString();
        TotalUseless = spendings.Where(s => s.IsUseful == false).Sum(s => s.Amount).ToString();
    }
}
