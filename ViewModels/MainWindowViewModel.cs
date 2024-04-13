using ReactiveUI;
namespace Bankable.ViewModels;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Extensions;
using Bankable.Services;
using System.Linq;

public class MainWindowViewModel : ViewModelBase
{
    private UserService userService = new();
    private CategoryService categoryService = new();
    private BankAccountService bankAccountService = new();
    private SpendingService spendingService = new();


    private ViewModelBase _contentViewModel;
    public ViewModelBase ContentViewModel
    {
        get => _contentViewModel;
        private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
    }

    private ViewModelBase _barsChartViewModel = new BarsChartViewModel();
    public ViewModelBase BarsChartViewModel
    {
        get => _barsChartViewModel;
        private set => this.RaiseAndSetIfChanged(ref _barsChartViewModel, value);
    }

    private ViewModelBase _donutChartViewModel = new DonutChartViewModel();
    public ViewModelBase DonutChartViewModel
    {
        get => _donutChartViewModel;
        private set => this.RaiseAndSetIfChanged(ref _donutChartViewModel, value);
    }

    public async void Home()
    {
        ContentViewModel = new HomeViewModel();
        await userService.AddItem(new Models.User { CreatedAt = DateTime.UtcNow, FirstName = "Mattéo", LastName = "Grellier" });
        var user = userService.GetAllItems().Result.First();
        await bankAccountService.AddItem(new Models.BankAccount { CreatedAt = DateTime.UtcNow, Amount = 56000, Description = "My main bank account", Name = "Main bank account", UserId = user.Id });
        var bankAccount = bankAccountService.GetAllItems().Result.First();
        // await categoryService.AddItem(new Models.Category { Name = "Manger" });
        // await categoryService.AddItem(new Models.Category { Name = "Dormir" });
        var singleCategory = categoryService.GetAllItems().Result.First();
        var secondCategory = categoryService.GetAllItems().Result.ElementAt(6);
        // await spendingService.AddItem(new Models.Spending { Amount = 10, Description = "My first spending", Date = DateTime.UtcNow, BankAccountId = bankAccount.Id, CategoryId = singleCategory.Id, IsUseful = true, IsRecurrent = true, Title = "First spending" });
        // await spendingService.AddItem(new Models.Spending { Amount = 50, Description = "My second spending", Date = DateTime.UtcNow, BankAccountId = bankAccount.Id, CategoryId = singleCategory.Id, IsUseful = true, IsRecurrent = true, Title = "Second spending" });
        await spendingService.AddItem(new Models.Spending { Amount = 20, Description = "encore", Date = DateTime.UtcNow, BankAccountId = bankAccount.Id, CategoryId = secondCategory.Id, IsUseful = true, IsRecurrent = false, Title = "plus" });
        await spendingService.AddItem(new Models.Spending { Amount = 20, Description = "encore mdr", Date = DateTime.UtcNow, BankAccountId = bankAccount.Id, CategoryId = secondCategory.Id, IsUseful = true, IsRecurrent = false, Title = "plus" });
    }

    public void BankAccounts()
    {
        ContentViewModel = new BankAccountsViewModel();
    }

    public void Savings()
    {
        ContentViewModel = new SavingsViewModel();
    }

    public void Start()
    {
        Series[0].Name = "Catégorie";
        Series[0].Values = valuesCollection;
        Random rnd = new Random();
        for (int i = 0; i < rnd.Next(5, 13); i++)
        {
            valuesCollection.Add(rnd.Next(1, 13));
        }
    }

    public ISeries[] Series {get; set;} = new ISeries[] { new LineSeries<double>() };

    private ObservableCollection<double> valuesCollection = new ObservableCollection<double>();

    public IEnumerable<ISeries> PieSeries { get; set; } =
        new[] { 2, 4, 1, 4, 3 }.AsPieSeries( (value, series) =>
        {
            series.MaxRadialColumnWidth = 60;
        });
}
