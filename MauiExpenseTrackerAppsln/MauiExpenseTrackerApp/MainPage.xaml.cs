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

        
    }
}
