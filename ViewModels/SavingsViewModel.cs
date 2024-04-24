using Bankable.ViewModels.Savings;
using ReactiveUI;

namespace Bankable.ViewModels;

public class SavingsViewModel: ViewModelBase
{
    private IDashboardListViewModel _contentViewModel;

    public SavingsViewModel()
    {
        ContentViewModel = new SavingsDashboardViewModel();
    }

    public IDashboardListViewModel ContentViewModel
    {
        get => _contentViewModel;
        private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
    }
    
    public void Dashboard()
    {
        ContentViewModel = new SavingsDashboardViewModel();
    }

    public void List()
    {
        ContentViewModel = new SavingsListViewModel();
    }
}