using System.Collections.Generic;
using System;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using LiveChartsCore.SkiaSharpView.Extensions;
using System.Drawing;
using Bankable.Models;
using Bankable.Services;

namespace Bankable.ViewModels;

public class DonutChartViewModel: ViewModelBase
{
    public DonutChartViewModel()
    {
        GetDataFromServices();
    }

    private static int _index = 0;
    private SpendingService _spendingService = new SpendingService();
    private CategoryService _categoryService = new CategoryService();

    public IEnumerable<ISeries> Series {get; set;}


    private async void GetDataFromServices()
    {
        IEnumerable<Category> categories = await _categoryService.GetAllItems();

        List<float> categoriesValues = new List<float>();
        List<string> categoriesNames = new List<string>();

        foreach (Category category in categories)
        {
            float totalPrice = 0;
            IEnumerable<Spending> listMonthSpendings = await _spendingService.GetAllSpendingsByCategorInMonth(category, DateTime.UtcNow);
            
            foreach (Spending spending in listMonthSpendings)
            {
                totalPrice += spending.Amount;
            }

            if (totalPrice > 0)
            {
                categoriesValues.Add(totalPrice);
                categoriesNames.Add(category.Name);
            }
        }

        Series = ConvertToPieSeries(categoriesValues, categoriesNames);
    }

    private IEnumerable<ISeries> ConvertToPieSeries(List<float> pricesList, List<string> categoriesNames)
    {
        if (pricesList.Count == categoriesNames.Count)
        {
            return pricesList.AsPieSeries( (value, series) => {
                series.Name = categoriesNames[_index++ % categoriesNames.Count];
                series.DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle;
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
        else
        {
            throw new ArgumentException("Both list aren't of the same size");
        }
    }
}