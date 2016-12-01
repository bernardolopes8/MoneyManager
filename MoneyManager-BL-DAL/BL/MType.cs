using System.Collections.ObjectModel;

namespace MoneyManager_BL_DAL
{
    public class MType
    {
        public long id { get; set; }
        public string name { get; set; }

        public static void CreateTable()
        {
            MTypeDAL.CreateTable();
        }

        public void Create()
        {
            if (RetrieveByName(this.name).Count == 0) MTypeDAL.Create(this);
        }

        public void Update()
        {
            MTypeDAL.Update(this);
        }

        public void Delete()
        {
            MTypeDAL.Delete(this);
        }

        public static ObservableCollection<MType> RetrieveAll()
        {
            return (MTypeDAL.RetrieveAll());
        }

        public static ObservableCollection<MType> RetrieveByName(string name)
        {
            return (MTypeDAL.RetrieveByName(name));
        }

        public static ObservableCollection<MType> RetrieveById(long id)
        {
            return (MTypeDAL.RetrieveById(id));
        }
    }
}
