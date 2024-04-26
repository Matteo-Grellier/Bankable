using Bankable.ViewModels.Dialogs;
using DialogHostAvalonia;

namespace Bankable.ViewModels;

public class NotAuthenticatedViewModel : ViewModelBase
{
    public async void Login()
    {
        if(DialogHost.IsDialogOpen("UserDialog"))
            DialogHost.Close("UserDialog");
        await DialogHost.Show(new LoginViewModel(), "UserDialog");
    }

    public async void Register()
    {
        if(DialogHost.IsDialogOpen("UserDialog"))
            DialogHost.Close("UserDialog");
        await DialogHost.Show(new RegisterViewModel(), "UserDialog");
    }
}