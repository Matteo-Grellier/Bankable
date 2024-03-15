using Bankable.ViewModels.Home;
using ReactiveUI;

namespace Bankable.ViewModels;

public class HomeViewModel: ViewModelBase
{
    private ViewModelBase _contentViewModel;

    public ViewModelBase ContentViewModel
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