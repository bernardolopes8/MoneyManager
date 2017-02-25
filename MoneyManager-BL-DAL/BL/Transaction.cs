using System;
using System.Collections.ObjectModel;

namespace MoneyManager_BL_DAL
{
    public class Transaction
    {
        public long id { get; set; }
        public double amount { get; set; }
        public DateTime date { get; set; }
        public string description { get; set; }
        public long category_id { get; set; }
        public long account_id { get; set; }
        public long type_id { get; set; }

        public static void CreateTable()
        {
            TransactionDAL.CreateTable();
        }

        public static object RetrieveByAccount()
        {
            throw new NotImplementedException();
        }

        public void Create()
        {
            TransactionDAL.Create(this);
        }

        public void Update()
        {
            TransactionDAL.Update(this);
        }

        public void Delete()
        {
            TransactionDAL.Delete(this);
        }

        public static ObservableCollection<Transaction> RetrieveById(string v)
        {
            throw new NotImplementedException();
        }

        public static object RetrieveById(ObservableCollection<Category> categoryName)
        {
            throw new NotImplementedException();
        }

        public static ObservableCollection<Transaction> RetrieveAll()
        {
            return (TransactionDAL.RetrieveAll());
        }

        public static ObservableCollection<Transaction> RetrieveById(long id)
        {
            return (TransactionDAL.RetrieveById(id));
        }

        public static ObservableCollection<Transaction> RetrieveByCategory(long category_id)
        {
            return (TransactionDAL.RetrieveByCategory(category_id));
        }

        public static ObservableCollection<Transaction> RetrieveByType(long type_id)
        {
            return (TransactionDAL.RetrieveByType(type_id));
        }

        public static ObservableCollection<Transaction> RetrieveByAccount(long account_id)
        {
            return (TransactionDAL.RetrieveByAccount(account_id));
        }

        public static ObservableCollection<Transaction> RetrieveByDateRange(DateTime startDate, DateTime endDate)
        {
            return (TransactionDAL.RetrieveByDateRange(startDate, endDate));
        }
    }
}