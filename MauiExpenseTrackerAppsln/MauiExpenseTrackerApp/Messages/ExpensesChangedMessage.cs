using CommunityToolkit.Mvvm.Messaging.Messages;

namespace MauiExpenseTrackerApp.Messages
{
    public class ExpensesChangedMessage : ValueChangedMessage<bool>
    {
        public ExpensesChangedMessage(bool value) : base(value) { }
    }
}
