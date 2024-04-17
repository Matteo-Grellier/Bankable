using ReactiveUI;
namespace Bankable.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
	private ViewModelBase _contentViewModel;

	public ViewModelBase ContentViewModel
	{
		get => _contentViewModel;
		private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
	}

	public void Home()
	{
		ContentViewModel = new HomeViewModel();
	}

	public void BankAccounts()
	{
		ContentViewModel = new BankAccountsViewModel();
	}

	public void Savings()
	{
		ContentViewModel = new SavingsViewModel();
	}
}
