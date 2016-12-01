using System.Collections.ObjectModel;

namespace MoneyManager_BL_DAL
{
    public class Currency
    {
        public long id { get; set; }
        public string name { get; set; }
        public double value { get; set; }

        public static void CreateTable()
        {
            CurrencyDAL.CreateTable();
        }

        public void Create()
        {
            if (RetrieveByName(this.name).Count == 0) CurrencyDAL.Create(this);
        }

        public void Update()
        {
            CurrencyDAL.Update(this);   
        }

        public void Delete()
        {
            CurrencyDAL.Delete(this);
        }

        public static ObservableCollection<Currency> RetrieveAll()
        {
            return (CurrencyDAL.RetrieveAll());
        }

        public static ObservableCollection<Currency> RetrieveByName(string name)
        {
            return (CurrencyDAL.RetrieveByName(name));
        }

        public static ObservableCollection<Currency> RetrieveById(long id)
        {
            return (CurrencyDAL.RetrieveById(id));
        }
    }
}

