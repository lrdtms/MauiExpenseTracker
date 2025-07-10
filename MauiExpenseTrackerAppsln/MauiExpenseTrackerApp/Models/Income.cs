using SQLite;

namespace MauiExpenseTrackerApp.Models
{
    public class Income
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public DateTime DateRecorded { get; set; } = DateTime.Now;
    }
}
