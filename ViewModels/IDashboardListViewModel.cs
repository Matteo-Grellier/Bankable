using System;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace Bankable.ViewModels;

public interface IDashboardListViewModel
{
    public DateTimeOffset SelectedDate { get; set; }

    private void OnDateChanged(DateTimeOffset date)
    {
        throw new NotImplementedException();
    }
}