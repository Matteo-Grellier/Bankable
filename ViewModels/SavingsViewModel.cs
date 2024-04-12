using Bankable.ViewModels.Savings;
using ReactiveUI;

namespace Bankable.ViewModels;

public class SavingsViewModel: ViewModelBase
{
    private ViewModelBase _contentViewModel;

    public ViewModelBase ContentViewModel
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