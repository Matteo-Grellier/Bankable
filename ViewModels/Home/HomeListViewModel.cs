using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Bankable.Views;

namespace Bankable.ViewModels.Home;

public class HomeListViewModel: ViewModelBase, IDashboardListViewModel
{
    public DateTimeOffset SelectedDate { get; set; }

    public HomeListViewModel()
    {
        SelectedDate = new DateTimeOffset(DateTime.Now);
    }
    
    private void OnDateChanged(DateTimeOffset date)
    {
        throw new NotImplementedException();
    }
}