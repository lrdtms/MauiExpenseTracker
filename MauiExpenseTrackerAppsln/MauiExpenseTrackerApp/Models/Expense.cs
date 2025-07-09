using SQLite;
using System;

namespace MauiExpenseTrackerApp.Models
{
    public class Expense
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Description { get; set; }
        public decimal Amount { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
    }
}
