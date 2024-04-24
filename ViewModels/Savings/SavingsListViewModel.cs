using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Bankable.Views;

namespace Bankable.ViewModels.Savings;

public class SavingsListViewModel: ViewModelBase, IDashboardListViewModel
{
    public DateTimeOffset SelectedDate { get; set; }

    public SavingsListViewModel()
    {
        SelectedDate = new DateTimeOffset(DateTime.Now);
    }
 
    private void OnDateChanged(DateTimeOffset date)
    {
        throw new NotImplementedException();
    }
}