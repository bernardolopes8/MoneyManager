using DataAbstractionLayerSQLite;
using SQLitePCL;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MoneyManager_BL_DAL
{
    class UserDAL
    {
        private static string databaseFile = "moneymanager.db";

        private static void Mapping(ISQLiteStatement statement, User e)
        {
            e.id = (long)statement["id"];
            e.login = (string)statement["login"];
            e.password = (string)statement["password"];
            e.currency_id = (long)statement["currency_id"];
        }

        public static void CreateTable()
        {
            string sql = @"CREATE TABLE IF NOT EXISTS ""user""(
                            ""id"" INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                            ""login"" VARCHAR(45) NOT NULL,
                            ""password"" VARCHAR(256) NOT NULL,
                            ""currency_id"" INTEGER,
                            CONSTRAINT ""login_UNIQUE"" UNIQUE(""login""),
                            CONSTRAINT ""fk_User_Currency1"" FOREIGN KEY(""currency_id"") REFERENCES ""currency""(""id"")
                            ON UPDATE CASCADE ON DELETE SET NULL
                         ); ";

            DB db = DB.getDB(databaseFile);
            db.NonQuery(sql);
        }
        
        public static void Create(User user)
        {
            string sql = @"INSERT INTO user (login, password, currency_id) 
                            VALUES (@login, @password, @currency_id)";

            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();
            
            string hashedPassword = PasswordHash.PasswordHash.CreateHash(user.password);
            
            parms.Add("@login", user.login);
            parms.Add("@password", hashedPassword);
            parms.Add("@currency_id", user.currency_id);
            if (db.NonQuery(sql, parms)) user.id = db.LastId();
        }

        public static void Update(User user)
        {
            string sql = @"UPDATE user
                            SET login=@login, password=@password, currency_id=@currency_id
                            WHERE id=@id";

            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            string hashedPassword = PasswordHash.PasswordHash.CreateHash(user.password);

            parms.Add("@login", user.login);
            parms.Add("@password", hashedPassword);
            parms.Add("@currency_id", user.currency_id);
            parms.Add("@id", user.id);
            db.NonQuery(sql, parms);
        }

        public static void Delete(User user)
        {   
            string sql = @"DELETE FROM user WHERE id=@id";

            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@id", user.id);
            db.NonQuery(sql, parms);
        }

        public static ObservableCollection<User> RetrieveAll()
        {
            ObservableCollection<User> res = new ObservableCollection<User>();

            string sql = "SELECT * FROM user";
            DB db = DB.getDB(databaseFile);
            ISQLiteStatement statement = db.Query(sql);

            while (statement.Step() == SQLiteResult.ROW)
            {
                User e = new User();

                Mapping(statement, e);
                res.Add(e);
            }

            return (res);
        }

        public static ObservableCollection<User> RetrieveByLogin(string login)
        {
            ObservableCollection<User> res = new ObservableCollection<User>();
            
            string sql = "SELECT * FROM user WHERE login=@login";
            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@login", login);
            ISQLiteStatement statement = db.Query(sql, parms);

            while (statement.Step() == SQLiteResult.ROW)
            {
                User e = new User();

                Mapping(statement, e);
                res.Add(e);
            }

            return (res);
        }

        public static ObservableCollection<User> RetrieveById(long id)
        {
            ObservableCollection<User> res = new ObservableCollection<User>();

            string sql = "SELECT * FROM user WHERE id=@id";
            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@id", id);
            ISQLiteStatement statement = db.Query(sql, parms);

            while (statement.Step() == SQLiteResult.ROW)
            {
                User e = new User();

                Mapping(statement, e);
                res.Add(e);
            }

            return (res);
        }
    }
}

