using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiExpenseTrackerApp.ViewModels
{
    public class BudgetPageViewModel : INotifyPropertyChanged
    {
        private string _incomeInput;
        private bool _isSubmitted = false;

        public string IncomeInput
        {
            get => _incomeInput;
            set
            {
                _incomeInput = value;
                OnPropertyChanged();
            }
        }

        public bool IsSubmitted
        {
            get => _isSubmitted;
            set
            {
                _isSubmitted = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}