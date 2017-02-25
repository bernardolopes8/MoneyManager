using MoneyManager_BL_DAL;
using System.Collections.ObjectModel;
using System;

namespace MoneyManager.ViewModel
{
    public class TransactionViewModel
    {
        public ObservableCollection<Transaction> Transactions { get; set; }
        public ObservableCollection<Transaction> AccountTransactions { get; set; }
        public ObservableCollection<Transaction> IncomeTransactions { get; set; }
        public ObservableCollection<Transaction> ExpenseTransactions { get; set; }

        public Transaction transaction { get; set; }

        public TransactionViewModel()
        {
            Transactions = Transaction.RetrieveAll();
            IncomeTransactions = Transaction.RetrieveByType(TypeViewModel.RetrieveTypeId("Income"));
            ExpenseTransactions = Transaction.RetrieveByType(TypeViewModel.RetrieveTypeId("Expense"));
        }

        internal void SetAccountTransactions(long account_id)
        {
            AccountTransactions = Transaction.RetrieveByAccount(account_id);
        }

        private void CollectionAdd()
        {
            if (transaction.type_id == TypeViewModel.RetrieveTypeId("Income"))
            {
                IncomeTransactions.Add(transaction);
            }
            else
            {
                ExpenseTransactions.Add(transaction);
            }

            Transactions.Add(transaction);
        }

        private void CollectionRemove()
        {
            if (IncomeTransactions.Contains(transaction))
            {
                IncomeTransactions.Remove(transaction);
            }
            else
            {
                ExpenseTransactions.Remove(transaction);
            }

            Transactions.Remove(transaction);
        }

        internal void Add()
        {
            transaction.Create();
            CollectionAdd();
        }

        internal void update()
        {
            CollectionRemove();
            transaction.Update();
            CollectionAdd();
        }

        internal void Delete()
        {
            CollectionRemove();
            transaction.Delete();
        }
    }
}
