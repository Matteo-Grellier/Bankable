using Bankable.Services;
using Bankable.ViewModels;
using Bankable.ViewModels.BankAccounts;
using Bankable.ViewModels.Dialogs;
using Bankable.ViewModels.Home;
using Bankable.ViewModels.Savings;
using Bankable.Views;
using Bankable.Views.BankAccounts;
using Bankable.Views.Dialogs;
using Bankable.Views.Home;
using Bankable.Views.Savings;
using Microsoft.Extensions.DependencyInjection;

namespace Bankable;

public static class ServiceCollectionExtensions {
    // Add ViewModels to the DI container
    public static void AddViewModels(this IServiceCollection collection) 
    {
        
        collection.AddSingleton<MainWindowViewModel>();
        collection.AddTransient<ViewModelBase>();
        collection.AddTransient<NotAuthenticatedViewModel>();
        collection.AddTransient<HomeViewModel>();
        collection.AddTransient<SavingsViewModel>();
        collection.AddTransient<BankAccountsViewModel>();
        collection.AddTransient<HomeDashboardViewModel>();
        collection.AddTransient<HomeListViewModel>();
        collection.AddTransient<SavingsDashboardViewModel>();
        collection.AddTransient<SavingsListViewModel>();
        collection.AddTransient<BankAccountsDashboardViewModel>();
        collection.AddTransient<BankAccountsListViewModel>();
        collection.AddTransient<AddBankAccountViewModel>();
        collection.AddTransient<AddIncomingViewModel>();
        collection.AddTransient<AddSpendingViewModel>();
        collection.AddTransient<AddSavingProjectViewModel>();
        collection.AddTransient<AddSavingViewModel>();
    }

    // Add Views to the DI container
    public static void AddViews(this IServiceCollection collection)
    {
        collection.AddTransient<NotAuthenticatedView>();
        collection.AddTransient<HomeView>();
        collection.AddTransient<SavingsView>();
        collection.AddTransient<BankAccountsView>();
        collection.AddTransient<HomeDashboardView>();
        collection.AddTransient<HomeListView>();
        collection.AddTransient<SavingsDashboardView>();
        collection.AddTransient<SavingsListView>();
        collection.AddTransient<BankAccountsDashboardView>();
        collection.AddTransient<BankAccountsListView>();
        collection.AddTransient<AddBankAccountView>();
        collection.AddTransient<AddIncomingView>();
        collection.AddTransient<AddSpendingView>();
        collection.AddTransient<AddSavingProjectView>();
        collection.AddTransient<AddSavingView>();
    }

    // Add Services to the DI container
    public static void AddServices(this IServiceCollection collection)
    {
        collection.AddSingleton<AuthenticationService>();
        collection.AddTransient<BankAccountService>();
        collection.AddTransient<CategoryService>();
        collection.AddTransient<IncomingService>();
        collection.AddTransient<SavingProjectService>();
        collection.AddTransient<SavingService>();
        collection.AddTransient<SpendingService>();
        collection.AddTransient<TokenService>();
        collection.AddTransient<UserService>();
    }
}