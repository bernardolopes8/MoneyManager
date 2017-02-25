using DataAbstractionLayerSQLite;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MoneyManager_BL_DAL
{
    class TransactionDAL
    {
        private static string databaseFile = "moneymanager.db";

        private static void Mapping(ISQLiteStatement statement, Transaction e)
        {
            e.id = (long)statement["id"];
            e.amount = (double)statement["amount"];
            e.date = DateTime.Parse((string)statement["date"]);
            e.description = (string)statement["description"];
            e.category_id = (long)statement["category_id"];
            e.account_id = (long)statement["account_id"];
            e.type_id = (long)statement["type_id"];
        }

        public static void CreateTable()
        {
            string sql = @"CREATE TABLE IF NOT EXISTS ""m_transaction""(
                            ""id"" INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                            ""amount"" FLOAT NOT NULL,
                            ""date"" DATETIME NOT NULL,
                            ""description"" VARCHAR(45),
                            ""category_id"" INTEGER,
                            ""account_id"" INTEGER,
                            ""type_id"" INTEGER,
                            CONSTRAINT ""fk_transaction_category1"" FOREIGN KEY(""category_id"") REFERENCES ""category""(""id"")
                            ON UPDATE CASCADE ON DELETE SET NULL,
                            CONSTRAINT ""fk_transaction_account1"" FOREIGN KEY(""account_id"") REFERENCES ""account""(""id"")
                            ON UPDATE CASCADE ON DELETE SET NULL,
                            CONSTRAINT ""fk_transaction_type1"" FOREIGN KEY(""type_id"") REFERENCES ""type""(""id"")
                            ON UPDATE CASCADE ON DELETE CASCADE
                         ); ";

            DB db = DB.getDB(databaseFile);
            db.NonQuery(sql);
        }

        public static void Create(Transaction transaction)
        {
            string sql = @"INSERT INTO m_transaction (amount, date, description, category_id, account_id, type_id) 
                            VALUES (@amount, @date, @description, @category_id, @account_id, @type_id)";

            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@amount", transaction.amount);
            parms.Add("@date", transaction.date.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            parms.Add("@description", transaction.description);
            parms.Add("@category_id", transaction.category_id);
            parms.Add("@account_id", transaction.account_id);
            parms.Add("@type_id", transaction.type_id);
            if (db.NonQuery(sql, parms)) transaction.id = db.LastId();
        }

        public static void Update(Transaction transaction)
        {
            string sql = @"UPDATE m_transaction
                            SET amount=@amount, date=@date, description=@description, 
                            category_id=@category_id, account_id=@account_id, type_id=@type_id
                            WHERE id=@id";

            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@amount", transaction.amount);
            parms.Add("@date", transaction.date.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            parms.Add("@description", transaction.description);
            parms.Add("@category_id", transaction.category_id);
            parms.Add("@account_id", transaction.account_id);
            parms.Add("@type_id", transaction.type_id);
            parms.Add("@id", transaction.id);
            db.NonQuery(sql, parms);
        }

        public static void Delete(Transaction transaction)
        {
            string sql = @"DELETE FROM m_transaction WHERE id=@id";

            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@id", transaction.id);
            db.NonQuery(sql, parms);
        }

        public static ObservableCollection<Transaction> RetrieveAll()
        {
            ObservableCollection<Transaction> res = new ObservableCollection<Transaction>();

            string sql = "SELECT * FROM m_transaction";
            DB db = DB.getDB(databaseFile);
            ISQLiteStatement statement = db.Query(sql);

            while (statement.Step() == SQLiteResult.ROW)
            {
                Transaction e = new Transaction();

                Mapping(statement, e);
                res.Add(e);
            }

            return (res);
        }

        public static ObservableCollection<Transaction> RetrieveById(long id)
        {
            ObservableCollection<Transaction> res = new ObservableCollection<Transaction>();

            string sql = "SELECT * FROM m_transaction WHERE id=@id";
            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@id", id);
            ISQLiteStatement statement = db.Query(sql, parms);

            while (statement.Step() == SQLiteResult.ROW)
            {
                Transaction e = new Transaction();

                Mapping(statement, e);
                res.Add(e);
            }

            return (res);
        }

        public static ObservableCollection<Transaction> RetrieveByCategory(long category_id)
        {
            ObservableCollection<Transaction> res = new ObservableCollection<Transaction>();

            string sql = "SELECT * FROM m_transaction WHERE category_id=@category_id";
            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@category_id", category_id);
            ISQLiteStatement statement = db.Query(sql, parms);

            while (statement.Step() == SQLiteResult.ROW)
            {
                Transaction e = new Transaction();

                Mapping(statement, e);
                res.Add(e);
            }

            return (res);
        }

        public static ObservableCollection<Transaction> RetrieveByType(long type_id)
        {
            ObservableCollection<Transaction> res = new ObservableCollection<Transaction>();

            string sql = "SELECT * FROM m_transaction WHERE type_id=@type_id ORDER BY date DESC";
            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@type_id", type_id);
            ISQLiteStatement statement = db.Query(sql, parms);

            while (statement.Step() == SQLiteResult.ROW)
            {
                Transaction e = new Transaction();

                Mapping(statement, e);
                res.Add(e);
            }

            return (res);
        }

        public static ObservableCollection<Transaction> RetrieveByAccount(long account_id)
        {
            ObservableCollection<Transaction> res = new ObservableCollection<Transaction>();

            string sql = "SELECT * FROM m_transaction WHERE account_id=@account_id ORDER BY date DESC";
            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@account_id", account_id);
            ISQLiteStatement statement = db.Query(sql, parms);

            while (statement.Step() == SQLiteResult.ROW)
            {
                Transaction e = new Transaction();

                Mapping(statement, e);
                res.Add(e);
            }

            return (res);
        }

        public static ObservableCollection<Transaction> RetrieveByDateRange(DateTime startDate, DateTime endDate)
        {
            ObservableCollection<Transaction> res = new ObservableCollection<Transaction>();

            string sql = "SELECT * FROM m_transaction WHERE date BETWEEN @startDate AND @endDate";
            DB db = DB.getDB(databaseFile);
            Dictionary<string, object> parms = new Dictionary<string, object>();

            parms.Add("@startDate", startDate.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            parms.Add("@endDate", endDate.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            ISQLiteStatement statement = db.Query(sql, parms);

            while (statement.Step() == SQLiteResult.ROW)
            {
                Transaction e = new Transaction();

                Mapping(statement, e);
                res.Add(e);
            }

            return (res);
        }
    }
}