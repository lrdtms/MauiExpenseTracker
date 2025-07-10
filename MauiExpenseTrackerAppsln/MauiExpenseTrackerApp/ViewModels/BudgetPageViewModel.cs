using System.ComponentModel;
using System.Runtime.CompilerServices;
using MauiExpenseTrackerApp.Models;
using MauiExpenseTrackerApp.Messages;
using CommunityToolkit.Mvvm.Messaging;

namespace MauiExpenseTrackerApp.ViewModels
{
    public class BudgetPageViewModel : INotifyPropertyChanged
    {
        private string _incomeInput;
        private bool _isSubmitted;
        private string _total;
        private string _remainderIncome;

        public string IncomeInput
        {
            get => _incomeInput;
            set
            {
                _incomeInput = value;
                OnPropertyChanged();
                SaveIncomeToDatabase(); // 💾 Save to SQLite
                UpdateTotal();          // 🧮 Update formatted total
            }
        }

        public bool IsSubmitted
        {
            get => _isSubmitted;
            set
            {
                _isSubmitted = value;
                OnPropertyChanged();
                Preferences.Set("IsSubmitted", value); // 💾 Save
            }
        }

        
        public string Total
        {
            get => _total;
            set
            {
                _total = value;
                OnPropertyChanged();
            }
        }

        public BudgetPageViewModel()
        {
            LoadIncomeFromDatabase();
            IsSubmitted = Preferences.Get("IsSubmitted", false); // ✅ Reload submission state
            _ = UpdateRemainderIncomeAsync();
            WeakReferenceMessenger.Default.Register<ExpensesChangedMessage>(this, async (r, m) =>
            {
                await UpdateRemainderIncomeAsync();
            });
        }

        private async void LoadIncomeFromDatabase()
        {
            var incomeRecord = await App.Database.GetLatestIncomeAsync();
            if (incomeRecord != null)
            {
                IncomeInput = incomeRecord.Amount.ToString();
            }
            else
            {
                IncomeInput = "0";
            }
            UpdateTotal();
        }

        private async void SaveIncomeToDatabase()
        {
            if (decimal.TryParse(_incomeInput, out decimal amount))
            {
                var newIncome = new Income
                {
                    Amount = amount,
                    DateRecorded = DateTime.Now
                };

                await App.Database.SaveIncomeAsync(newIncome); // ✅ Save to SQLite
                await UpdateRemainderIncomeAsync();
            }
        }

        private void UpdateTotal()
        {
            if (decimal.TryParse(_incomeInput, out decimal income))
            {
                Total = $"R{income:N2}";
            }
            else
            {
                Total = "R0.00";
            }
        }
        public string RemainderIncome
        {
            get => _remainderIncome;
            set
            {
                _remainderIncome = value;
                OnPropertyChanged();
            }
        }

        public async Task UpdateRemainderIncomeAsync()
        {
            if (!decimal.TryParse(IncomeInput, out decimal income))
                income = 0;

            var expenses = await App.Database.GetExpensesAsync();
            var totalExpenses = expenses.Sum(e => e.Amount);

            var remainder = income - totalExpenses;
            RemainderIncome = $"R{remainder:N2}";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}