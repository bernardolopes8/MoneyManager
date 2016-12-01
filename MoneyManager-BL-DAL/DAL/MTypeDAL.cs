using DataAbstractionLayerSQLite;
using SQLitePCL;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MoneyManager_BL_DAL
{
    class MTypeDAL
    {
        private static string databaseFile = "moneymanager.db";

        private static void Mapping(ISQLiteStatement statement, MType e)
        {
            e.id = (long)statement["id"];
            e.name = (string)statement["name"];
        }

        public static void CreateTable()
        {
            string sql = @"CREATE TABLE IF NOT EXISTS ""type""(
                            ""id"" INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                            ""name"" VARCHAR(45) NOT NULL,
                            CONSTRAINT ""name_UNIQUE"" UNIQUE(""name"")
                         ); ";

            DB db = DB.getDB(databaseFile);
            db.NonQuery(sql);
        }

        public static void Create(MType type)
        {
            string sql = @"INSERT INTO type (name) 
                            VALUES (@name)";

            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@name", type.name);
            if (db.NonQuery(sql, parms)) type.id = db.LastId();
        }

        public static void Update(MType type)
        {
            string sql = @"UPDATE type
                            SET name=@name
                            WHERE id=@id";

            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@name", type.name);
            parms.Add("@id", type.id);
            db.NonQuery(sql, parms);
        }

        public static void Delete(MType type)
        {
            string sql = @"DELETE FROM type WHERE id=@id";

            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@id", type.id);
            db.NonQuery(sql, parms);
        }

        public static ObservableCollection<MType> RetrieveAll()
        {
            ObservableCollection<MType> res = new ObservableCollection<MType>();

            string sql = "SELECT * FROM type";
            DB db = DB.getDB(databaseFile);
            ISQLiteStatement statement = db.Query(sql);

            while (statement.Step() == SQLiteResult.ROW)
            {
                MType e = new MType();

                Mapping(statement, e);
                res.Add(e);
            }

            return (res);
        }

        public static ObservableCollection<MType> RetrieveByName(string name)
        {
            ObservableCollection<MType> res = new ObservableCollection<MType>();

            string sql = "SELECT * FROM type WHERE name=@name";
            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@name", name);
            ISQLiteStatement statement = db.Query(sql, parms);

            while (statement.Step() == SQLiteResult.ROW)
            {
                MType e = new MType();

                Mapping(statement, e);
                res.Add(e);
            }

            return (res);
        }

        public static ObservableCollection<MType> RetrieveById(long id)
        {
            ObservableCollection<MType> res = new ObservableCollection<MType>();

            string sql = "SELECT * FROM type WHERE id=@id";
            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@id", id);
            ISQLiteStatement statement = db.Query(sql, parms);

            while (statement.Step() == SQLiteResult.ROW)
            {
                MType e = new MType();

                Mapping(statement, e);
                res.Add(e);
            }

            return (res);
        }
    }
}
