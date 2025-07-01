
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiExpenseTrackerApp.Models;
using System.Collections.ObjectModel;

namespace MauiExpenseTrackerApp.ViewModels
{
    public partial class ExpenseViewModel : ObservableObject
    {
        private readonly Page _page;

        public ExpenseViewModel(Page page)
        {
            _page = page;
        }

        public ObservableCollection<Expense> Expenses { get; set; } = new();

        [ObservableProperty]
        decimal total;

        [RelayCommand]
        public async Task ShowAddExpensePopupAsync()
        {
            string description = await _page.DisplayPromptAsync("New Expense", "Enter a description:");

            if (string.IsNullOrWhiteSpace(description))
                return;

            string amountStr = await _page.DisplayPromptAsync("Amount", "Enter amount spent (e.g. 120.50):", keyboard: Keyboard.Numeric);

            if (decimal.TryParse(amountStr, out decimal amount) && amount > 0)
            {
                Expenses.Add(new Expense { Description = description, Amount = amount });
                Total = Expenses.Sum(e => e.Amount);
            }
            else
            {
                await _page.DisplayAlert("Invalid Input", "Amount must be a valid number greater than zero.", "OK");
            }
        }

        [RelayCommand]
        void DeleteExpense(Expense expense)
        {
            if (Expenses.Contains(expense))
            {
                Expenses.Remove(expense);
                Total = Expenses.Sum(e => e.Amount);
            }
        }
    }
}
