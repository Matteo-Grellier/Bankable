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
    private readonly BankAccountService _bankAccountServiceService = new();
    
    private DateTimeOffset _selectedDate;
    private List<Spending> _spendings;
    private List<Incoming> _incomes;

    public BankAccountsDashboardViewModel()
    {
        _selectedDate = new DateTimeOffset(DateTime.Now);
        SetValues(_selectedDate);
        SetLast3spendingsAsync();
        SetBalanceAsync();
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

    public string TotalIncomings 
    {
        get
        {
            List<Incoming> allIncomings = _incomingService.GetItemsForUser();
            double total = 0;
            foreach (Incoming incoming in allIncomings)
            {
                total += incoming.Amount;
            }
            return total + "€";
        }
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
        if (allSpendings.Count() < 3) { throw new Exception("not enough elements in array"); }
        int index = allSpendings.Count() - 1;
        stringList.Add(allSpendings[index].Title + " - " + allSpendings[index].Amount + "€");
        stringList.Add(allSpendings[index -1].Title + " - " + allSpendings[index -1].Amount + "€");
        stringList.Add(allSpendings[index -2].Title + " - " + allSpendings[index -2].Amount + "€");
        Last3spendings = stringList;
    }
    
    private string _balance = "0€";
    public string Balance
    {
        get => _balance;
        private set => this.RaiseAndSetIfChanged(ref _balance, value);
    }

    private async void SetBalanceAsync()
    {
        List<BankAccount> allSpendings = await _bankAccountServiceService.GetItemsByUser();
        double total = 0;
        foreach (BankAccount bankAccount in allSpendings)
        {
            total += bankAccount.Amount;
        }
        Balance = total + "€";
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
