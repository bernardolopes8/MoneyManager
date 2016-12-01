using DataAbstractionLayerSQLite;
using SQLitePCL;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MoneyManager_BL_DAL
{
    class CategoryDAL
    {
        private static string databaseFile = "moneymanager.db";

        private static void Mapping(ISQLiteStatement statement, Category e)
        {
            e.id = (long)statement["id"];
            e.name = (string)statement["name"];
        }

        public static void CreateTable()
        {
            string sql = @"CREATE TABLE IF NOT EXISTS ""category""(
                            ""id"" INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                            ""name"" VARCHAR(45) NOT NULL,
                            CONSTRAINT ""name_UNIQUE"" UNIQUE(""name"")
                         ); ";

            DB db = DB.getDB(databaseFile);
            db.NonQuery(sql);
        }

        public static void Create(Category category)
        {
            string sql = @"INSERT INTO category (name) 
                            VALUES (@name)";

            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@name", category.name);
            if (db.NonQuery(sql, parms)) category.id = db.LastId();
        }

        public static void Update(Category category)
        {
            string sql = @"UPDATE category
                            SET name=@name
                            WHERE id=@id";

            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@name", category.name);
            parms.Add("@id", category.id);
            db.NonQuery(sql, parms);
        }

        public static void Delete(Category category)
        {
            string sql = @"DELETE FROM category WHERE id=@id";

            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@id", category.id);
            db.NonQuery(sql, parms);
        }

        public static ObservableCollection<Category> RetrieveAll()
        {
            ObservableCollection<Category> res = new ObservableCollection<Category>();

            string sql = "SELECT * FROM category";
            DB db = DB.getDB(databaseFile);
            ISQLiteStatement statement = db.Query(sql);

            while (statement.Step() == SQLiteResult.ROW)
            {
                Category e = new Category();

                Mapping(statement, e);
                res.Add(e);
            }

            return (res);
        }

        public static ObservableCollection<Category> RetrieveByName(string name)
        {
            ObservableCollection<Category> res = new ObservableCollection<Category>();

            string sql = "SELECT * FROM category WHERE name=@name";
            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@name", name);
            ISQLiteStatement statement = db.Query(sql, parms);

            while (statement.Step() == SQLiteResult.ROW)
            {
                Category e = new Category();

                Mapping(statement, e);
                res.Add(e);
            }

            return (res);
        }

        public static ObservableCollection<Category> RetrieveById(long id)
        {
            ObservableCollection<Category> res = new ObservableCollection<Category>();

            string sql = "SELECT * FROM category WHERE id=@id";
            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@id", id);
            ISQLiteStatement statement = db.Query(sql, parms);

            while (statement.Step() == SQLiteResult.ROW)
            {
                Category e = new Category();

                Mapping(statement, e);
                res.Add(e);
            }

            return (res);
        }
    }
}
