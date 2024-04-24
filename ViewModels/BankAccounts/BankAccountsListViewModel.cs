using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Bankable.Views;

namespace Bankable.ViewModels.BankAccounts;

public class BankAccountsListViewModel: ViewModelBase, IDashboardListViewModel
{
    public DateTimeOffset SelectedDate { get; set; }

    public BankAccountsListViewModel()
    {
        SelectedDate = new DateTimeOffset(DateTime.Now);
    }
    
    private void OnDateChanged(DateTimeOffset date)
    {
        throw new NotImplementedException();
    }
}