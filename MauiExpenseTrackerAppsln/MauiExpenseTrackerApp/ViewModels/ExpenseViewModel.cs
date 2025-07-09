
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
            LoadExpenses(); // 🔄 Load from SQLite when ViewModel is created
        }

        public ObservableCollection<Expense> Expenses { get; set; } = new();

        [ObservableProperty]
        decimal total;

        private async void LoadExpenses()
        {
            var expensesFromDb = await App.Database.GetExpensesAsync();

            Expenses.Clear();
            foreach (var expense in expensesFromDb)
                Expenses.Add(expense);

            Total = Expenses.Sum(e => e.Amount);
        }

        [RelayCommand]
        public async Task ShowAddExpensePopupAsync()
        {
            string description = await _page.DisplayPromptAsync("New Expense", "Enter a description:");

            if (string.IsNullOrWhiteSpace(description))
                return;

            string amountStr = await _page.DisplayPromptAsync("Amount", "Enter amount spent (e.g. 120.50):", keyboard: Keyboard.Numeric);

            if (decimal.TryParse(amountStr, out decimal amount) && amount > 0)
            {
                var newExpense = new Expense
                {
                    Description = description,
                    Amount = amount,
                    Date = DateTime.Now
                };

                await App.Database.AddExpenseAsync(newExpense); // 💾 Save to DB
                Expenses.Add(newExpense);                      // 🧠 Add to list
                Total = Expenses.Sum(e => e.Amount);           // 🔄 Update total
            }
            else
            {
                await _page.DisplayAlert("Invalid Input", "Amount must be a valid number greater than zero.", "OK");
            }
        }

        [RelayCommand]
        public async void DeleteExpense(Expense expense)
        {
            if (Expenses.Contains(expense))
            {
                await App.Database.DeleteExpenseAsync(expense); // ❌ Remove from DB
                Expenses.Remove(expense);                      // ❌ Remove from UI
                Total = Expenses.Sum(e => e.Amount);           // 🔄 Update total
            }
        }
    }
}
