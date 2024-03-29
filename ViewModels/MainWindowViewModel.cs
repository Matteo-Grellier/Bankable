using ReactiveUI;
namespace Bankable.ViewModels;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Extensions;

public class MainWindowViewModel : ViewModelBase
{
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

    public void Home()
    {
        ContentViewModel = new HomeViewModel();
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
