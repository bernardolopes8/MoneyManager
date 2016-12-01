using SQLitePCL;
using System;
using System.Collections.Generic;

namespace DataAbstractionLayerSQLite
{
    public class DB : IDisposable
    {
        
        private bool isDisposed = false;
        private SQLiteConnection conn = null;
        private static DB db = null;

        private DB(string file)
        {



            conn = new SQLiteConnection(file);
            var query = @"PRAGMA foreign_keys = ON";
            this.NonQuery(query, null);
            
        }

        public static DB getDB(string file)
        {
            if (db == null)
            {
                db = new DB(file);
            }
            return (db);
        }

        protected void Dispose(bool disposing)
        {
            if (isDisposed) return;
            if (disposing)
            {
                if (conn != null)
                {
                    conn.Dispose();
                }
            }
            conn = null;
            isDisposed = true;
        }



        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DB()
        {
            Dispose(false);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="query">Expects ? for parameter or @name</param>
        /// <param name="parms"></param>
        /// <returns></returns>
        /// 
        public bool NonQuery(string query, Dictionary<string, object> parms=null)
        {
            bool res = false;
            using (ISQLiteStatement com = this.conn.Prepare(query))
            {
                if (parms != null)
                {
                    foreach (KeyValuePair<string, object> kvp in parms)
                    {
                        com.Bind(kvp.Key, kvp.Value);
                    }
                }
                res = com.Step() ==SQLiteResult.DONE ;
            }

            return (res);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query">Expects ? for parameter or @name\n returns</param>
        /// <param name="parms"></param>
        /// <returns></returns>
        /// 
        public ISQLiteStatement Query(string query, Dictionary<string, object> parms=null)
        {
            ISQLiteStatement com = this.conn.Prepare(query);
            
            if (parms != null)
            {
                foreach (KeyValuePair<string, object> kvp in parms)
                {
                    com.Bind(kvp.Key, kvp.Value);
                }
            }
            

            return (com);
        }

        public long LastId()
        {
            long res = conn.LastInsertRowId();
            
            return (res);
        }
        
    }
}
