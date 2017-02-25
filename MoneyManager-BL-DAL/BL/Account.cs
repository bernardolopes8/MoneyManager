using System.Collections.ObjectModel;

namespace MoneyManager_BL_DAL
{
    public class Account
    {
        public long id { get; set; }
        public string name { get; set; }
        public double balance { get; set; }

        public static void CreateTable()
        {
            AccountDAL.CreateTable();
        }

        public void Create()
        {
            if (RetrieveByName(this.name).Count == 0) AccountDAL.Create(this);
        }

        public void Update()
        {
            AccountDAL.Update(this);
        }

        public void Delete()
        {
            AccountDAL.Delete(this);
        }

        public static ObservableCollection<Account> RetrieveAll()
        {
            return (AccountDAL.RetrieveAll());
        }

        public static ObservableCollection<Account> RetrieveByName(string name)
        {
            return (AccountDAL.RetrieveByName(name));
        }

        public static ObservableCollection<Account> RetrieveById(long id)
        {
            return (AccountDAL.RetrieveById(id));
        }
    }
}
