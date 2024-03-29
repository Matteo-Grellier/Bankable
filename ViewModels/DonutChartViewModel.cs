using System.Collections.Generic;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using LiveChartsCore.SkiaSharpView.Extensions;
using System.Drawing;

namespace Bankable.ViewModels;

public class DonutChartViewModel: ViewModelBase
{
    private static int _index = 0;
    private static string[] _categories = new[] { "Alimentaire", "Transport", "Logement", "Loisir", "George" };

    public IEnumerable<ISeries> Series { get; set; } =
        new[] { 127, 26, 568, 57, 10 }.AsPieSeries((value, series) =>
        {
            series.Name = _categories[_index++ % _categories.Length];
            series.DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle; //LiveChartsCore.Measure.PolarLabelsPosition.Outer; 
            series.DataLabelsPaint = new SolidColorPaint(SKColors.White)
            {
                SKTypeface = SKTypeface.FromFamilyName("Arial", SKFontStyle.Bold)
            };
            series.DataLabelsSize = 15;
            series.DataLabelsFormatter = point => $"{point.StackedValue!.Share:P2}";
            series.MaxRadialColumnWidth = 100;
            series.ToolTipLabelFormatter = point => $"{point.Coordinate.PrimaryValue}€ ({point.StackedValue!.Share:P2})";
        });
}