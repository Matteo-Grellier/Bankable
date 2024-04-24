using System;
using System.Collections.Generic;
using Bankable.Models;
using Bankable.Services;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using Bankable.ViewModels.BankAccounts;
using ReactiveUI;
using LiveChartsCore.Measure;

namespace Bankable.ViewModels;

public class CompletionChartViewModel: ViewModelBase
{
    public CompletionChartViewModel()
    {
        GetDataFromServices();
    }
    
    private SavingService _savingsService = new();
    private SavingProjectService _savingProjectsService = new();
    
    
    public ISeries[] Series { get; set; } =
    {
        new StackedRowSeries<double>
        {
            Name = "Saved",
            Values = new List<double> { 10, 25, 40 },
            Stroke = null,
            Fill = new SolidColorPaint(new SKColor(255, 209, 102)),
            DataLabelsPaint = new SolidColorPaint(new SKColor(50, 50, 50)),
            DataLabelsSize = 14,
            DataLabelsPosition = DataLabelsPosition.Middle,
            DataLabelsFormatter = point => $"{point.StackedValue!.Share:P0}",
            StackGroup = 0
        },
        new StackedRowSeries<double>
        {
            Name = "Remaining",
            Values = new List<double> { 90, 75, 60 },
            Stroke = null,
            Fill = new SolidColorPaint(new SKColor(239, 71, 111)),
            DataLabelsPaint = new SolidColorPaint(new SKColor(250, 250, 250)),
            DataLabelsSize = 14,
            DataLabelsPosition = DataLabelsPosition.Middle,
            DataLabelsFormatter = point => $"{point.StackedValue!.Share:P0}",
            StackGroup = 0
        },
    };
    
    public Axis[] YAxes { get; set; } = new Axis[] {
        new Axis {
            Labels = new string[] { "Italie", "Velo", "Cadeau"},
            LabelsRotation = 0,
            TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35)),
            TicksAtCenter = true,
            ForceStepToMin = true, 
            MinStep = 1 
        }
    };
    
    private async void GetDataFromServices()
    {

        List<SavingProject> savingsProjectList = await _savingProjectsService.GetAll();
        List<String> savingProjectsNames = new ();
        List<double> remainingAmountPerProject = new ();
        List<double> savingAmountPerProject = new ();
        
        foreach (SavingProject savingProject in savingsProjectList)
        {
            savingProjectsNames.Add(savingProject.Title);
            
            double total = 0;
            IEnumerable<Saving> savingsInCategory = await _savingsService.GetItemsBySavingProject(savingProject.Id);
            foreach (Saving saving in savingsInCategory)
            {
                total += saving.Amount;
            }

            double percent = total / savingProject.FinalAmount * 100;
            savingAmountPerProject.Add(percent);
            remainingAmountPerProject.Add(100 - percent);
        }

        Series[0].Values = savingAmountPerProject;
        Series[1].Values = remainingAmountPerProject;
        YAxes[0].Labels = savingProjectsNames;
    }
}