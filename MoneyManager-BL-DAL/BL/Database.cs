

namespace MoneyManager_BL_DAL.BL
{
    public class Database
    {
        public static void CreateTables()
        {
            MType.CreateTable();
            Category.CreateTable();
            Account.CreateTable();
            Debt.CreateTable();
            Transaction.CreateTable();
        }
        
    }
}
