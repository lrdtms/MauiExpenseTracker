using MauiExpenseTrackerApp.Services;

namespace MauiExpenseTrackerApp
{
    public partial class App : Application
    {
        public static DatabaseService Database { get; private set; }

        public App()
        {
            InitializeComponent();

            string dbPath = GetDatabasePath();      // 👈 Use it here
            Database = new DatabaseService(dbPath); // 👈 Initialize here

            MainPage = new MainPage(); // or MainPage, depending on your layout
        }

        

        private static string GetDatabasePath()    // 👈 Add it here
        {
            string filename = "expenses.db";
            string folderPath = FileSystem.AppDataDirectory;
            return Path.Combine(folderPath, filename);
        }
    }
}