using System.Collections.Generic;
using System.Collections.ObjectModel;
using Bankable.Models;
using Bankable.ViewModels;

namespace AvaloniaControls.ViewModels
{
public class BankAccountsListViewModel : ViewModelBase
    {
        public ObservableCollection<Spending> People { get; }

    public BankAccountsListViewModel()
        {
            var spending = new List<Spending>
            {
                new Spending(),
            };
            People = new ObservableCollection<Spending>(spending);
        }
    }
}
