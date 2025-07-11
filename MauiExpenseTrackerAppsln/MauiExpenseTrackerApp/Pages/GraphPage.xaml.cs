using CommunityToolkit.Mvvm.Messaging;
using MauiExpenseTrackerApp.Messages;
using MauiExpenseTrackerApp.Models;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes; // For Visibility enum
using Syncfusion.Maui.Charts;          // Add this namespace
using System.Collections.ObjectModel;

namespace MauiExpenseTrackerApp.Pages;

public partial class GraphPage : ContentPage
{
    public ObservableCollection<ExpenseChartItem> ExpenseData { get; set; } = new();

    public GraphPage()
    {
        InitializeComponent();
        BindingContext = this;
        // Subscribe to expense updates
        WeakReferenceMessenger.Default.Register<ExpensesChangedMessage>(this, async (r, m) =>
        {
            await LoadExpenseData(); // ?? Rebuild chart when expenses change
        });

        _ = LoadExpenseData(); // Load initially
    }

    private async Task   LoadExpenseData()
    {
        var expenses = await App.Database.GetExpensesAsync();
        var totalExpenses = expenses.Sum(e => e.Amount);

        var latestIncome = await App.Database.GetLatestIncomeAsync();
        var income = latestIncome?.Amount ?? 0;

        var remainder = income - totalExpenses;
        var savings = remainder * 0.10m;
        var leftover = remainder - savings;

        // Clear existing data
        ExpenseData.Clear();

        // Add total expenses as a single slice
        ExpenseData.Add(new ExpenseChartItem
        {
            Category = "Expenses",
            Amount = (double)totalExpenses
        });

        // Add savings
        ExpenseData.Add(new ExpenseChartItem
        {
            Category = "Savings (10%)",
            Amount = (double)savings
        });

        // Add leftover
        ExpenseData.Add(new ExpenseChartItem
        {
            Category = "Leftover",
            Amount = (double)leftover
        });

        // After adding data, set the label display properties
        SetPieSeriesLabelSettings();
    }

    private void SetPieSeriesLabelSettings()
    {
        var pieSeries = PieChart.Series[0] as PieSeries;
        if (pieSeries != null)
        {
            pieSeries.ShowDataLabels = true;

            // Just enable connector lines, no label position
            pieSeries.DataLabelSettings = new CircularDataLabelSettings
            {
                UseSeriesPalette = true
            };
        }
    }
}

public class ExpenseChartItem
{
    public string Category { get; set; }
    public double Amount { get; set; }
    public string DisplayLabel => $"{Category}: R{Amount:N2}";
}