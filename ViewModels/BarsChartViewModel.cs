using System;
using System.Collections.Generic;
using Bankable.Models;
using Bankable.Services;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace Bankable.ViewModels;

public class BarsChartViewModel: ViewModelBase
{
    public BarsChartViewModel()
    {
        GetDataFromServices();
    }

    private SolidColorPaint[] _colors =
    {
        new SolidColorPaint(new SKColor(6, 214, 160)),      // green
        new SolidColorPaint(new SKColor(255, 209, 102)),    // red
        new SolidColorPaint(new SKColor(239, 71, 111)),     // yellow
        new SolidColorPaint(new SKColor(179, 133, 255)),    // purple
    };
        
    private SavingService _savingsService = new();
    private SavingProjectService _savingProjectsService = new();

    public ISeries[] Series { get; set; } =
    {
        new StackedColumnSeries<double>
        {
            Name = "Italie",
            Values = new double[] { 1000, 1100, 1000, 200, 1000, 300},
            Fill = new SolidColorPaint(new SKColor(6, 214, 160)),
            StackGroup = 0
        },
        new StackedColumnSeries<double>
        {
            Name = "Voiture",
            Values = new double[] { 256, 681, 452, 100, 655, 212},
            Fill = new SolidColorPaint(new SKColor(239, 71, 111)),
            StackGroup = 0
        },
        new StackedColumnSeries<double>
        {
            Name = "Projet 2",
            Values = new double[] { 300, 130, 24, 24, 0, 340},
            Fill = new SolidColorPaint(new SKColor(255, 209, 102)),
            StackGroup = 0
        }
    };

    public Axis[] XAxes { get; set; } = new Axis[] {
        new Axis {
            Labels = new string[] { "January", "February", "March", "April", "May", "June" },
            LabelsRotation = 0,
            TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35)),
            TicksAtCenter = true,
            ForceStepToMin = true, 
            MinStep = 1 
        }
    };
    
    
    private async void GetDataFromServices()
    {
        List<int> last6MonthsList = new();
        int currentMonth = DateTime.UtcNow.Month;
        for (int i = 0; i <= 5; i++)
        {
            last6MonthsList.Add( (currentMonth - i > 0) ? currentMonth - i : 12 + (currentMonth - i));
        }
        last6MonthsList.Reverse();

        List<SavingProject> savingsProjectList;
        //List<SavingProject> savingsProjectList = await _savingProjectsService.GetItemsForUser();
        savingsProjectList = await _savingProjectsService.GetAll();
        
        List<string> monthNames = new ();
        ISeries[] columnsSeries = new ISeries[savingsProjectList.Count];
        
        int index = 0;
        foreach (SavingProject savingProject in savingsProjectList)
        {
            List<double> savingProjectValues = new();
            foreach (int month in last6MonthsList)
            {
                float total = 0;
                IEnumerable<Saving> savingsThisMonthandCategory = await _savingsService.GetAllSavingsInMonthBySavingProject(month, savingProject.Id);
                foreach (Saving saving in savingsThisMonthandCategory)
                {
                    total += saving.Amount;
                }
                savingProjectValues.Add(total);

                if (month > 0 && month < 13)
                {
                    DateTime placeholderDate = new DateTime(2000, month, 1);
                    monthNames.Add(placeholderDate.ToString("MMMM"));
                }
                else
                {
                    throw new Exception("Month number is out of range");
                }
            }

            StackedColumnSeries<double> newColumn = new StackedColumnSeries<double>
            {
                Name= savingProject.Title,
                Values = savingProjectValues,
                Fill = _colors[index % _colors.Length],
                StackGroup = 0
            };

            columnsSeries[index] = newColumn;
            index++;
        }

        Series = columnsSeries;
        XAxes[0].Labels = monthNames;
    }
}