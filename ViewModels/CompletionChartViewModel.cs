using Bankable.ViewModels.BankAccounts;
using ReactiveUI;

namespace Bankable.ViewModels;

public class BankAccountsViewModel: ViewModelBase
{
    private ViewModelBase _contentViewModel;

    public ViewModelBase ContentViewModel
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