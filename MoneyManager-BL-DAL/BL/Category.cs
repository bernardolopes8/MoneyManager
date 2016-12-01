using System.Collections.ObjectModel;

namespace MoneyManager_BL_DAL
{
    public class Category
    {
        public long id { get; set; }
        public string name { get; set; }

        public static void CreateTable()
        {
            CategoryDAL.CreateTable();
        }

        public void Create()
        {
            if (RetrieveByName(this.name).Count == 0) CategoryDAL.Create(this);
        }

        public void Update()
        {
            CategoryDAL.Update(this);
        }

        public void Delete()
        {
            CategoryDAL.Delete(this);
        }

        public static ObservableCollection<Category> RetrieveAll()
        {
            return (CategoryDAL.RetrieveAll());
        }

        public static ObservableCollection<Category> RetrieveByName(string name)
        {
            return (CategoryDAL.RetrieveByName(name));
        }

        public static ObservableCollection<Category> RetrieveById(long id)
        {
            return (CategoryDAL.RetrieveById(id));
        }
    }
}