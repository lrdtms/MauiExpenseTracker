using MauiExpenseTrackerApp.ViewModels;
using Microsoft.Maui.Controls;
using System;

namespace MauiExpenseTrackerApp.Pages
{ 

    public partial class BudgetPage : ContentPage
    {
    private BudgetPageViewModel viewModel;
    public BudgetPage()
	{
		InitializeComponent();
        viewModel = new BudgetPageViewModel();
        BindingContext = viewModel;
    }
    private void OnIncomeEntryCompleted(object sender, EventArgs e)
    {
        viewModel.IsSubmitted = true;
    }
        private void OnIncomeLabelTapped(object sender, EventArgs e)
        {
            if (BindingContext is BudgetPageViewModel vm)
            {
                vm.IsSubmitted = !vm.IsSubmitted;
            }
        }
    }
}