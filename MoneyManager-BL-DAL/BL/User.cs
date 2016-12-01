using System.Collections.ObjectModel;

namespace MoneyManager_BL_DAL
{
    public class User
    {
        public long id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public long currency_id { get; set; }

        public static void CreateTable()
        {
            UserDAL.CreateTable();
        }

        public void Create()
        {
            if (RetrieveByLogin(this.login).Count == 0) UserDAL.Create(this);
        }

        public void Update()
        {
            UserDAL.Update(this);   
        }

        public void Delete()
        {
            UserDAL.Delete(this);
        }

        public static ObservableCollection<User> RetrieveAll()
        {
            return (UserDAL.RetrieveAll());
        }

        public static ObservableCollection<User> RetrieveByLogin(string login)
        {
            return (UserDAL.RetrieveByLogin(login));
        }

        public static ObservableCollection<User> RetrieveById(long id)
        {
            return (UserDAL.RetrieveById(id));
        }
    }
}

