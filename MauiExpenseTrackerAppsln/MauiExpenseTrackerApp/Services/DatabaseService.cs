using SQLite;
using MauiExpenseTrackerApp.Models;

namespace MauiExpenseTrackerApp.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Expense>().Wait();
            _database.CreateTableAsync<Income>().Wait();
        }

        public Task<List<Expense>> GetExpensesAsync()
        {
            return _database.Table<Expense>().ToListAsync();
        }

        public Task<int> AddExpenseAsync(Expense expense)
        {
            return _database.InsertAsync(expense);
        }

        public Task<int> DeleteExpenseAsync(Expense expense)
        {
            return _database.DeleteAsync(expense);
        }

        public Task<int> UpdateExpenseAsync(Expense expense)
        {
            return _database.UpdateAsync(expense);
        }
        public Task<List<Income>> GetIncomeAsync() =>
        _database.Table<Income>().OrderByDescending(i => i.DateRecorded).ToListAsync();

        public Task<Income> GetLatestIncomeAsync() =>
            _database.Table<Income>().OrderByDescending(i => i.DateRecorded).FirstOrDefaultAsync();

        public Task<int> SaveIncomeAsync(Income income) =>
            _database.InsertAsync(income);
    }
}
