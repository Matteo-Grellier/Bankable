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
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.Measure;

namespace Bankable.ViewModels;

public class GaugeChartViewModel : ViewModelBase
{
	public GaugeChartViewModel()
	{
		DebugGetDataFromServices();
	}

	private BankAccountService _banAccountService = new BankAccountService();

	public IEnumerable<ISeries> Series { get; set; }


	public async void GetDataFromServices(Guid bankAccountID, float moneyForTheMonth)
	{
		BankAccount actualBankAccount = await _banAccountService.GetItemByID(bankAccountID);
		Series = ConvertToGaugeSeries(actualBankAccount.Amount, moneyForTheMonth);
	}


	private async void DebugGetDataFromServices()
	{
		try
		{
			IEnumerable<BankAccount> actualBankAccount = await _banAccountService.GetAllItems();
			GetDataFromServices(actualBankAccount.First().Id, 200000);
		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
		}
	}

	private IEnumerable<ISeries> ConvertToGaugeSeries(float moneyAmount, float moneyForTheMonth)
	{
		float percentage = (int)(moneyAmount / moneyForTheMonth * 100);
		SKColor gaugeColor;

		if (percentage > 50)
			gaugeColor = new SKColor(6, 214, 160);
		else if (percentage > 30)
			gaugeColor = new SKColor(255, 209, 102);
		else
			gaugeColor = new SKColor(239, 71, 111);

		return GaugeGenerator.BuildSolidGauge(
			new GaugeItem(percentage, series =>
			{
				series.Fill = new SolidColorPaint(gaugeColor);
				series.DataLabelsSize = 50;
				series.DataLabelsPaint = new SolidColorPaint(SKColors.Black);
				series.DataLabelsPosition = PolarLabelsPosition.ChartCenter;
				series.InnerRadius = 100;
				series.DataLabelsFormatter = point => $"{point.Coordinate.PrimaryValue} %";
				series.ToolTipLabelFormatter = point => $"{moneyAmount}€ ({point.Coordinate.PrimaryValue} % of {moneyForTheMonth})";
			}),
			new GaugeItem(GaugeItem.Background, series =>
			{
				series.InnerRadius = 100;
				series.Fill = new SolidColorPaint(new SKColor(100, 181, 246, 90));
			})
		);
	}
}
