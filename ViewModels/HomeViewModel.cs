using Bankable.ViewModels.Home;
using ReactiveUI;

namespace Bankable.ViewModels;

public class HomeViewModel: ViewModelBase
{
    private IDashboardListViewModel _contentViewModel;

    public HomeViewModel()
    {
        ContentViewModel = new HomeDashboardViewModel();
    }

    public IDashboardListViewModel ContentViewModel
    {
        get => _contentViewModel;
        private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
    }
    public void Dashboard()
    {
        ContentViewModel = new HomeDashboardViewModel();
    }

    public void List()
    {
        ContentViewModel = new HomeListViewModel();
    }
}