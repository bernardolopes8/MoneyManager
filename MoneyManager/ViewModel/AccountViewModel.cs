using MoneyManager_BL_DAL;
using System.Collections.ObjectModel;
using System.Linq;

namespace MoneyManager.ViewModel
{
    public class AccountViewModel
    {

        public ObservableCollection<Account> Accounts { get; set; }

        public Account account { get; set; }

        public AccountViewModel()
        {
            Accounts = Account.RetrieveAll();
        }

        internal void Add()
        {
            account.Create();
            Accounts.Add(account);
        }

        internal void update()
        {
            Accounts.Remove(account);
            account.Update();
            Accounts.Add(account);
        }

        internal void delete()
        {
            Accounts.Remove(account);
            account.Delete();
        }

        public static string getName(long account_id)
        {
            return Account.RetrieveById(account_id).First().name;
        }
    }

}
