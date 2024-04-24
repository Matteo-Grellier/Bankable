using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Bankable.Views;
using ReactiveUI;

namespace Bankable.ViewModels.BankAccounts;

public class BankAccountsDashboardViewModel: ViewModelBase, IDashboardListViewModel
{
    private DateTimeOffset _date;

    public BankAccountsDashboardViewModel()
    {
        SelectedDate = new DateTimeOffset(DateTime.Now);
    }
    public DateTimeOffset SelectedDate 
    { 
        get => _date;
        set
        {
            OnDateChanged(value);
            this.RaiseAndSetIfChanged(ref _date, value);
        }
    }

    private void OnDateChanged(DateTimeOffset date)
    {
        Console.WriteLine(date);
    }
}