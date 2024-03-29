using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace Bankable.ViewModels;

public class BarsChartViewModel: ViewModelBase
{
    private string[] _monthList = new string[] { "January", "February", "March" };


    public ISeries[] Series { get; set; } =
    {
        new ColumnSeries<double>
        {
            Name = "Earnings",
            Values = new double[] { 1000, 1100, 1000},
            Fill = new SolidColorPaint(new SKColor(6, 214, 160))
        },
        new ColumnSeries<double>
        {
            Name = "Expenses",
            Values = new double[] { 256, 681, 452 },
            Fill = new SolidColorPaint(new SKColor(239, 71, 111))
        },
        new ColumnSeries<double>
        {
            Name = "Savings",
            Values = new double[] { 300, 130, 24 },
            Fill = new SolidColorPaint(new SKColor(255, 209, 102))
        }
    };

    public Axis[] XAxes { get; set; } = new Axis[] {
        new Axis {
            Labels = new string[] { "January", "February", "March" },
            LabelsRotation = 0,
            SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200)),
            SeparatorsAtCenter = false,
            TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35)), //35 35 35
            TicksAtCenter = true,
            ForceStepToMin = true, 
            MinStep = 1 
        }
    };


    public void PopulateBarsChart(string[] monthList, double[] earnings, double[] expenses, double[] savings)
    {
        if (monthList.Length == earnings.Length && monthList.Length == expenses.Length && monthList.Length == savings.Length)
        {
            XAxes[0].Labels = monthList;
            Series[0].Values = earnings;
            Series[1].Values = expenses;
            Series[1].Values = savings;
        }
    }
}