using System;
using System.Linq;
using Bankable.Services;
using ReactiveUI;
namespace Bankable.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
	private ViewModelBase _contentViewModel = new();

	private UserService userService = new();

	private CategoryService categoryService = new();

	private BankAccountService bankAccountService = new();

	private SpendingService spendingService = new();

	public ViewModelBase ContentViewModel
	{
		get => _contentViewModel;
		private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
	}

	public void Home()
	{
		ContentViewModel = new HomeViewModel();
		// await userService.AddItem(new Models.User { CreatedAt = DateTime.UtcNow, FirstName = "Mattéo", LastName = "Grellier" });
		// var user = userService.GetAllItems().Result.First();
		// await bankAccountService.AddItem(new Models.BankAccount { CreatedAt = DateTime.UtcNow, Amount = 56000, Description = "My main bank account", Name = "Main bank account", UserId = user.Id });
		// var bankAccount = bankAccountService.GetAllItems().Result.First();
		// await categoryService.AddItem(new Models.Category { Name = "Manger" });
		// var singleCategory = categoryService.GetAllItems().Result.First();
		// await spendingService.AddItem(new Models.Spending { Amount = 10, Description = "My first spending", Date = DateTime.UtcNow, BankAccountId = bankAccount.Id, CategoryId = singleCategory.Id, IsUseful = true, IsRecurrent = true, Title = "First spending" });
		// await spendingService.AddItem(new Models.Spending { Amount = 50, Description = "My second spending", Date = DateTime.UtcNow, BankAccountId = bankAccount.Id, CategoryId = singleCategory.Id, IsUseful = true, IsRecurrent = true, Title = "Second spending" });
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
