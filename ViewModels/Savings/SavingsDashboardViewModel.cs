using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Bankable.Models;
using Bankable.Services;
using Bankable.Views;
using ReactiveUI;

namespace Bankable.ViewModels.Savings;

public class SavingsDashboardViewModel: ViewModelBase, IDashboardListViewModel
{

    private readonly SavingService _savingsService = new();
    private readonly SavingProjectService _savingProjectService = new();
    public DateTimeOffset SelectedDate { get; set; }

    public SavingsDashboardViewModel()
    {
        SelectedDate = new DateTimeOffset(DateTime.Now);
        SetAllData();
    }

    private ViewModelBase _completionChartViewModel = new CompletionChartViewModel();
    public ViewModelBase CompletionChartViewModel
    {
        get => _completionChartViewModel;
        private set => this.RaiseAndSetIfChanged(ref _completionChartViewModel, value);
    }

    private ViewModelBase _barsChartViewModel = new BarsChartViewModel();
    public ViewModelBase BarsChartViewModel
    {
        get => _barsChartViewModel;
        private set => this.RaiseAndSetIfChanged(ref _barsChartViewModel, value);
    }


    private void OnDateChanged(DateTimeOffset date)
    {
        throw new NotImplementedException();
    }

    private string _totalSaved;
    public string TotalSaved
    {
        get => _totalSaved;
        set => this.RaiseAndSetIfChanged(ref _totalSaved, value);
    }

    private string _totalToAcheve;
    public string TotalToAcheve
    {
        get => _totalToAcheve;
        set => this.RaiseAndSetIfChanged(ref _totalToAcheve, value);
    }

    private List<Saving> _last3savings;
    public List<Saving> Last3savings
    {
        get => _last3savings;
        set => this.RaiseAndSetIfChanged(ref _last3savings, value);
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

    private SavingProject _nextSavingProject;
    public SavingProject NextSavingProject
    {
        get => _nextSavingProject;
        set => this.RaiseAndSetIfChanged(ref _nextSavingProject, value);
    }

    private string _remainingMonths;
    public string RemainingMonths
    {
        get => _remainingMonths;
        set => this.RaiseAndSetIfChanged(ref _remainingMonths, value);
    }

    private async void SetAllData()
    {
        var allSavings = await _savingsService.GetAllByUser();
        var allSavingProjects = await _savingProjectService.GetAll();

        // Get all saved money from all time
        TotalSaved = allSavings.Sum(s => s.Amount).ToString();


        // Get the 3 last savings
        List<Saving> firstSavings;
        firstSavings = allSavings.OrderByDescending(s => s.Date).Take(3).ToList();
        firstSavings.ForEach(saving =>
        {
            saving.SavingProject = _savingProjectService.GetById(saving.SavingProjectId).Result;
        });
        Last3savings = firstSavings;

        // Get all saving goals for all projects
        TotalToAcheve = allSavingProjects.Sum(s => s.FinalAmount).ToString();

        // get all savings from this month
        DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
        SavedThisMonth = allSavings.Where(
            s => s.Date >= firstDayOfMonth && s.Date <= lastDayOfMonth
            ).Sum(s => s.Amount).ToString();

        DateTime currentDate = DateTime.Now;

        // get all savings from this year
        DateTime firstDayOfYear = new DateTime(currentDate.Year, 1, 1);
        DateTime lastDayOfYear = new DateTime(currentDate.Year, 12, 31);

        var savingsThisYear = allSavings.Where(s => s.Date >= firstDayOfYear && s.Date <= lastDayOfYear).ToList();
        SavedThisYear = savingsThisYear.Sum(s => s.Amount).ToString();

        // get remaining months for the next project
        var orderedSavingProjects = allSavingProjects.OrderBy(p => p.WillEndAt).ToList();
        if (orderedSavingProjects.Count() > 0)
        {
            NextSavingProject = orderedSavingProjects[0];
            TimeSpan difference = NextSavingProject.WillEndAt - currentDate;
            int remainingMonths = (int)(difference.TotalDays / (365.25 / 12));
            RemainingMonths = Math.Max(0, remainingMonths).ToString();
        }
        else
        {
            NextSavingProject = new SavingProject();
            RemainingMonths = "No project";
        }


    }
}
