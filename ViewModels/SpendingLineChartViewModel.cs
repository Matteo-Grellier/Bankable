using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.VisualElements;
using LiveChartsCore.Defaults;
using System.Collections.Generic;
using Bankable.Models;
using Bankable.Services;
using System;
using System.Linq;

namespace Bankable.ViewModels;

public class SpendingLineChartViewModel: ViewModelBase
{
    public SpendingLineChartViewModel()
    {
        GetDataFromServices();
    }


    private SpendingService _spendingService = new SpendingService();


    public ISeries[] Series { get; set; } =
    {
        new LineSeries<float>
        {
            Values = new float[] { 1040, 212, 573, 343, 1845, 237},
            Fill = null,
            Stroke = new SolidColorPaint(new SKColor(239, 71, 111), 4),
            GeometryStroke = new SolidColorPaint(new SKColor(239, 71, 111), 4),
            YToolTipLabelFormatter = point => $"-{point.PrimaryValue} €"
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

    public LabelVisual Title { get; set; } =
    new LabelVisual
    {
        Text = "Last 6 month spendings",
        TextSize = 25,
        Padding = new LiveChartsCore.Drawing.Padding(15),
        Paint = new SolidColorPaint(new SKColor(6, 214, 160))
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

        List<float> spendingSeries = new();
        List<string> monthNames = new ();

        foreach (int month in last6MonthsList)
        {
            float total = 0;
            IEnumerable<Spending> monthSpendingsList = await _spendingService.GetAllSpendingsInMonth(month);
            
            foreach (Spending spending in monthSpendingsList)
            {
                total += spending.Amount;
            }
            spendingSeries.Add(total);

            if (month > 0 && month < 13)
            {
                DateTime placeholderDate = new DateTime(2000, month, 1);
                monthNames.Add(placeholderDate.ToString("MMMM"));
            }
            else 
            {
                monthNames.Add("error mdr");
            }
        }

        Series[0].Values = spendingSeries;
        XAxes[0].Labels = monthNames;
    }
}