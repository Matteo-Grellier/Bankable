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
    public class DonutCategory
    {
        public string Name { get; set;}
        public float TotalPrice { get; set;}
    }

    public DonutChartViewModel()
    {
        Console.WriteLine("alo mec ? ?????????????"); 
        SetListMonthSpending();
    }

    private static int _index = 0;
    private static List<string> _categories = new List<string> { "Alimentaire", "Transport", "Logement", "Loisir", "George" };
    
    private SpendingService _spendingService = new SpendingService();
    private CategoryService _categoryService = new CategoryService();
    private List<DonutCategory> _donutCategoryList = new List<DonutCategory>();


    public IEnumerable<ISeries> Series { get; set; } = new[] { 127, 26, 568, 57, 10 }.AsPieSeries( (value, series) => {
        series.Name = _categories[_index++ % _categories.Count];
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
    // { 
    //     get {return Series;}
    //     set {  
    //         value.AsPieSeries( (value, series) => {
    //             series.Name = _categories[_index++ % _categories.Length];
    //             series.DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle;
    //             series.DataLabelsPaint = new SolidColorPaint(SKColors.White)
    //             {
    //                 SKTypeface = SKTypeface.FromFamilyName("Arial", SKFontStyle.Bold)
    //             };
    //             series.DataLabelsSize = 15;
    //             series.DataLabelsFormatter = point => $"{point.StackedValue!.Share:P2}";
    //             series.MaxRadialColumnWidth = 100;
    //             series.ToolTipLabelFormatter = point => $"{point.Coordinate.PrimaryValue}€ ({point.StackedValue!.Share:P2})";
    //         });
    //     }
    // }

    private async void SetListMonthSpending()
    {
        CategoryService categoryService = new CategoryService();
        IEnumerable<Category> categories = await _categoryService.GetAllItems();
        
        List<float> categoriesValues = new List<float>();
        List<string> categoriesNames = new List<string>();

        foreach (Category category in categories)
        {
            float totalPrice = 0;
            categoriesNames.Add(category.Name);

            IEnumerable<Spending> listMonthSpendings = await _spendingService.GetAllSpendingsByCategorInMonth(category, DateTime.UtcNow);
            
            foreach (Spending spending in listMonthSpendings)
            {
                totalPrice += spending.Amount;
            }

            // categoriesValues.Add(totalPrice);
            _categories = categoriesNames;
            _donutCategoryList.Add(new DonutCategory { Name = category.Name, TotalPrice = totalPrice});
        }

        Series = categoriesValues.AsPieSeries( (value, series) => {
            series.Name = _categories[_index++ % _categories.Count];
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
}