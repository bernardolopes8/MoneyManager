using System;
using System.Collections.ObjectModel;

namespace MoneyManager_BL_DAL
{
    public class Debt
    {
        public long id { get; set; }
        public double amount { get; set; }
        public DateTime deadline { get; set; }
        public string description { get; set; }
        public long category_id { get; set; }
        public long type_id { get; set; }    

        public static void CreateTable()
        {
            DebtDAL.CreateTable();
        }

        public void Create()
        {
            DebtDAL.Create(this);
        }

        public void Update()
        {
            DebtDAL.Update(this);
        }

        public void Delete()
        {
            DebtDAL.Delete(this);
        }

        public static ObservableCollection<Debt> RetrieveAll()
        {
            return (DebtDAL.RetrieveAll());
        }

        public static ObservableCollection<Debt> RetrieveById(long id)
        {
            return (DebtDAL.RetrieveById(id));
        }

        public static ObservableCollection<Debt> RetrieveByCategory(long category_id)
        {
            return (DebtDAL.RetrieveByCategory(category_id));
        }

        public static ObservableCollection<Debt> RetrieveByType(long type_id)
        {
            return (DebtDAL.RetrieveByType(type_id));
        }

        public static ObservableCollection<Debt> RetrieveByDeadline(DateTime deadline)
        {
            return (DebtDAL.RetrieveByDeadline(deadline));
        }
    }
}
