using Bankable.ViewModels.BankAccounts;
using Bankable.Views;
using ReactiveUI;

namespace Bankable.ViewModels;

public class BankAccountsViewModel: ViewModelBase
{
    private IDashboardListViewModel _contentViewModel;

    public BankAccountsViewModel()
    {
        ContentViewModel = new BankAccountsDashboardViewModel();
    }

    public IDashboardListViewModel ContentViewModel
    {
        get => _contentViewModel;
        private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
    }

    public void Dashboard()
    {
        ContentViewModel = new BankAccountsDashboardViewModel();
    }

    public void List()
    {
        ContentViewModel = new BankAccountsListViewModel();
    }
}