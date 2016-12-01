using DataAbstractionLayerSQLite;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MoneyManager_BL_DAL
{
    class DebtDAL
    {
        private static string databaseFile = "moneymanager.db";

        private static void Mapping(ISQLiteStatement statement, Debt e)
        {
            e.id = (long)statement["id"];
            e.amount = (double)statement["amount"];
            e.deadline = DateTime.Parse((string)statement["deadline"]);
            e.description = (string)statement["description"];
            e.user_id = (long)statement["user_id"];
            e.category_id = (long)statement["category_id"];
            e.type_id = (long)statement["type_id"];
        }

        public static void CreateTable()
        {
            string sql = @"CREATE TABLE IF NOT EXISTS ""debt""(
                            ""id"" INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                            ""amount"" FLOAT NOT NULL,
                            ""deadline"" DATETIME,
                            ""description"" VARCHAR(45),
                            ""user_id"" INTEGER NOT NULL,
                            ""category_id"" INTEGER,
                            ""type_id"" INTEGER,
                            CONSTRAINT ""fk_debt_user1"" FOREIGN KEY(""user_id"") REFERENCES ""user""(""id"")
                            ON UPDATE CASCADE ON DELETE CASCADE,
                            CONSTRAINT ""fk_debt_category1"" FOREIGN KEY(""category_id"") REFERENCES ""category""(""id"")
                            ON UPDATE CASCADE ON DELETE SET NULL,
                            CONSTRAINT ""fk_debtn_type1"" FOREIGN KEY(""type_id"") REFERENCES ""type""(""id"")
                            ON UPDATE CASCADE ON DELETE SET NULL
                         ); ";

            DB db = DB.getDB(databaseFile);
            db.NonQuery(sql);
        }

        public static void Create(Debt debt)
        {
            string sql = @"INSERT INTO debt (amount, deadline, description, user_id, category_id, type_id) 
                            VALUES (@amount, @deadline, @description, @user_id, @category_id, @type_id)";

            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@amount", debt.amount);
            parms.Add("@deadline", debt.deadline.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            parms.Add("@description", debt.description);
            parms.Add("@user_id", debt.user_id);
            parms.Add("@category_id", debt.category_id);
            parms.Add("@type_id", debt.type_id);
            db.NonQuery(sql, parms);
        }

        public static void Update(Debt debt)
        {
            string sql = @"UPDATE debt
                            SET amount=@amount, deadline=@deadline, description=@description, user_id=@user_id, 
                            category_id=@category_id, type_id=@type_id
                            WHERE id=@id";

            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@amount", debt.amount);
            parms.Add("@deadline", debt.deadline.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            parms.Add("@description", debt.description);
            parms.Add("@user_id", debt.user_id);
            parms.Add("@category_id", debt.category_id);
            parms.Add("@type_id", debt.type_id);
            parms.Add("@id", debt.id);
            if (db.NonQuery(sql, parms)) debt.id = db.LastId();
        }

        public static void Delete(Debt debt)
        {
            string sql = @"DELETE FROM debt WHERE id=@id";

            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@id", debt.id);
            db.NonQuery(sql, parms);
        }

        public static ObservableCollection<Debt> RetrieveAll()
        {
            ObservableCollection<Debt> res = new ObservableCollection<Debt>();

            string sql = "SELECT * FROM debt";
            DB db = DB.getDB(databaseFile);
            ISQLiteStatement statement = db.Query(sql);

            while (statement.Step() == SQLiteResult.ROW)
            {
                Debt e = new Debt();

                Mapping(statement, e);
                res.Add(e);
            }

            return (res);
        }

        public static ObservableCollection<Debt> RetrieveById(long id)
        {
            ObservableCollection<Debt> res = new ObservableCollection<Debt>();

            string sql = "SELECT * FROM debt WHERE id=@id";
            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@id", id);
            ISQLiteStatement statement = db.Query(sql, parms);

            while (statement.Step() == SQLiteResult.ROW)
            {
                Debt e = new Debt();

                Mapping(statement, e);
                res.Add(e);
            }

            return (res);
        }

        public static ObservableCollection<Debt> RetrieveByUser(long user_id)
        {
            ObservableCollection<Debt> res = new ObservableCollection<Debt>();

            string sql = "SELECT * FROM debt WHERE user_id=@user_id";
            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@user_id", user_id);
            ISQLiteStatement statement = db.Query(sql, parms);

            while (statement.Step() == SQLiteResult.ROW)
            {
                Debt e = new Debt();

                Mapping(statement, e);
                res.Add(e);
            }

            return (res);
        }

        public static ObservableCollection<Debt> RetrieveByCategory(long category_id)
        {
            ObservableCollection<Debt> res = new ObservableCollection<Debt>();

            string sql = "SELECT * FROM debt WHERE category_id=@category_id";
            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@category_id", category_id);
            ISQLiteStatement statement = db.Query(sql, parms);

            while (statement.Step() == SQLiteResult.ROW)
            {
                Debt e = new Debt();

                Mapping(statement, e);
                res.Add(e);
            }

            return (res);
        }

        public static ObservableCollection<Debt> RetrieveByType(long type_id)
        {
            ObservableCollection<Debt> res = new ObservableCollection<Debt>();

            string sql = "SELECT * FROM debt WHERE type_id=@type_id";
            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@type_id", type_id);
            ISQLiteStatement statement = db.Query(sql, parms);

            while (statement.Step() == SQLiteResult.ROW)
            {
                Debt e = new Debt();

                Mapping(statement, e);
                res.Add(e);
            }

            return (res);
        }

        public static ObservableCollection<Debt> RetrieveByDeadline(DateTime deadline)
        {
            ObservableCollection<Debt> res = new ObservableCollection<Debt>();

            string sql = "SELECT * FROM debt WHERE deadline<=@deadline";
            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@deadline", deadline.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            ISQLiteStatement statement = db.Query(sql, parms);

            while (statement.Step() == SQLiteResult.ROW)
            {
                Debt e = new Debt();

                Mapping(statement, e);
                res.Add(e);
            }

            return (res);
        }
    }
}