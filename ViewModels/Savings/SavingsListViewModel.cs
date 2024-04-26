using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Bankable.Views;
using System.Collections.Generic;
using Bankable.Models;
using Bankable.Services;
using ReactiveUI;

namespace Bankable.ViewModels.Savings;

public class SavingsListViewModel: ViewModelBase, IDashboardListViewModel
{
    private readonly SavingService _savingsService = new();
    private readonly SavingProjectService _savingProjectService = new();
    private readonly CategoryService _categoryService = new();

    private IEnumerable<SavingProject> _savingProjects;
    public DateTimeOffset SelectedDate { get; set; }

    public SavingsListViewModel()
    {
        GetSavingProjects();
        SelectedDate = new DateTimeOffset(DateTime.Now);
    }

    private void OnDateChanged(DateTimeOffset date)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<SavingProject> SavingProjects
    {
        get => _savingProjects;
        set => this.RaiseAndSetIfChanged(ref _savingProjects, value);
    }

    private async void GetSavingProjects()
    {
        SavingProjects = await _savingProjectService.GetItemsForUser();

        foreach(var savingProject in SavingProjects)
        {
            savingProject.Savings = await _savingsService.GetItemsBySavingProject(savingProject.Id);
        }

    }
}
