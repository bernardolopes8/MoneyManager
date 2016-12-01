using DataAbstractionLayerSQLite;
using SQLitePCL;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MoneyManager_BL_DAL
{
    class CurrencyDAL
    {
        private static string databaseFile = "moneymanager.db";

        private static void Mapping(ISQLiteStatement statement, Currency e)
        {
            e.id = (long)statement["id"];
            e.name = (string)statement["name"];
            e.value = (double)statement["value"];
        }

        public static void CreateTable()
        {
            string sql = @"CREATE TABLE IF NOT EXISTS ""currency""(
                            ""id"" INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                            ""name"" VARCHAR(45) NOT NULL,
                            ""value"" FLOAT NOT NULL,
                            CONSTRAINT ""name_UNIQUE"" UNIQUE(""name"")
                         ); ";

            DB db = DB.getDB(databaseFile);
            db.NonQuery(sql);
        }
        
        public static void Create(Currency currency)
        {
            string sql = @"INSERT INTO currency (name, value) 
                            VALUES (@name, @value)";

            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();
           
            parms.Add("@name", currency.name);
            parms.Add("@value", currency.value);
            if (db.NonQuery(sql, parms)) currency.id = db.LastId();
        }

        public static void Update(Currency currency)
        {
            string sql = @"UPDATE currency
                            SET name=@name, value=@value
                            WHERE id=@id";

            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@name", currency.name);
            parms.Add("@value", currency.value);
            parms.Add("@id", currency.id);
            db.NonQuery(sql, parms);
        }

        public static void Delete(Currency currency)
        {   
            string sql = @"DELETE FROM currency WHERE id=@id";

            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@id", currency.id);
            db.NonQuery(sql, parms);
        }

        public static ObservableCollection<Currency> RetrieveAll()
        {
            ObservableCollection<Currency> res = new ObservableCollection<Currency>();

            string sql = "SELECT * FROM currency";
            DB db = DB.getDB(databaseFile);
            ISQLiteStatement statement = db.Query(sql);

            while (statement.Step() == SQLiteResult.ROW)
            {
                Currency e = new Currency();

                Mapping(statement, e);
                res.Add(e);
            }

            return (res);
        }

        public static ObservableCollection<Currency> RetrieveByName(string name)
        {
            ObservableCollection<Currency> res = new ObservableCollection<Currency>();
            
            string sql = "SELECT * FROM currency WHERE name=@name";
            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@name", name);
            ISQLiteStatement statement = db.Query(sql, parms);

            while (statement.Step() == SQLiteResult.ROW)
            {
                Currency e = new Currency();

                Mapping(statement, e);
                res.Add(e);
            }

            return (res);
        }

        public static ObservableCollection<Currency> RetrieveById(long id)
        {
            ObservableCollection<Currency> res = new ObservableCollection<Currency>();

            string sql = "SELECT * FROM currency WHERE id=@id";
            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@id", id);
            ISQLiteStatement statement = db.Query(sql, parms);

            while (statement.Step() == SQLiteResult.ROW)
            {
                Currency e = new Currency();

                Mapping(statement, e);
                res.Add(e);
            }

            return (res);
        }
    }
}

