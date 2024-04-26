using System;

namespace Bankable.ViewModels.BankAccounts;

public interface IListVIewModel
{
    public DateTimeOffset SelectedDate { get; set; }
}