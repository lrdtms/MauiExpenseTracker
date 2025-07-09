using MauiExpenseTrackerApp.Models;
using MauiExpenseTrackerApp.ViewModels;

namespace MauiExpenseTrackerApp
{
    public partial class MainPage : ContentPage
    {
        

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new ExpenseViewModel(this);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            

            // Get all expenses
            var allExpenses = await App.Database.GetExpensesAsync();

            // Example: Display in console (for testing)
            foreach (var expense in allExpenses)
            {
                Console.WriteLine($"Description: {expense.Description}, Amount: {expense.Amount}, Date: {expense.Date}");
            }
        }
    }
}
