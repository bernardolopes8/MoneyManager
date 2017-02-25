using MoneyManager.ViewModel;
using MoneyManager_BL_DAL;
using System;
using Windows.UI.Core;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using MoneyManager.Views.Dialogs.Add;
using Windows.UI.Popups;
using MoneyManager.Views.Dialogs.Update;
using MoneyManager.Views.Pages;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MoneyManager.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AccountsPage : Page
    {
        public SettingsHandler SettingsHandler { get; set; }
        public AccountViewModel AccountViewModel { get; set; }
        public TransactionViewModel TransactionViewModel { get; set; }

        public object Header { get; set; }
        public object MainListBox { get; private set; }
        public object MyFrame { get; private set; }

        public AccountsPage()
        {
            this.InitializeComponent();
            this.SettingsHandler = new SettingsHandler();
            this.AccountViewModel = new AccountViewModel();
            this.TransactionViewModel = new TransactionViewModel();
        }

        private void HomeRadioButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HamburgerButton_Click.IsSelected)
            {
                MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;

            }

            if (Settings_Click.IsSelected)
            {

                this.Frame.Navigate(typeof(SettingsPage));
            }

            if (Currency_Click.IsHitTestVisible)
            {
                Currency_Click.IsEnabled = false;
            }

            if (Debt_Click.IsSelected)
            {
                this.Frame.Navigate(typeof(DebtsPage));
            }

            if (Transactions_Click.IsSelected)
            {
                this.Frame.Navigate(typeof(TransactionsPage));
            }

            if (Accounts_Click.IsSelected)
            {
                this.Frame.Navigate(typeof(AccountsPage));
            }

            if (Categories_Click.IsSelected)
            {
                this.Frame.Navigate(typeof(CategoriesPage));
            }

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var currentView = SystemNavigationManager.GetForCurrentView();

            currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;

        }


        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            var currentView = SystemNavigationManager.GetForCurrentView();

            currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

        }

        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            AccountViewModel.account = new Account();
            AddAccountDialog md = new AddAccountDialog(AccountViewModel);
            await md.ShowAsync();

        }

        private async void Update_Click(Object sender, RoutedEventArgs e)
        {
            FrameworkElement fe = sender as FrameworkElement;
            if (fe != null)
            {
                Account a = fe.DataContext as Account;
                if (a != null)
                {
                    AccountViewModel.account = a;
                    UpdateAccountDialog am = new UpdateAccountDialog(AccountViewModel);
                    await am.ShowAsync();
                }
            }

        }

        private async void Delete_Click(Object sender, RoutedEventArgs e)
        {
            FrameworkElement fe = sender as FrameworkElement;
            if (fe != null)
            {
                Account a = fe.DataContext as Account;
                if (a != null)
                {
                    if (AccountViewModel.Accounts.Count() == 1)
                    {
                        MessageDialog md = new MessageDialog("Invalid operation. There must be at least one account!");
                        await md.ShowAsync();
                    }
                    else if (Transaction.RetrieveByAccount(a.id).Count() != 0){
                        MessageDialog md = new MessageDialog("Account can't be deleted because there are transactions associated!");
                        await md.ShowAsync();
                    }
                    else
                    {
                        AccountViewModel.account = a;
                        AccountViewModel.delete();
                    }
                }
            }
        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement fe = sender as FrameworkElement;
            if (fe != null)
            {
                Account a = fe.DataContext as Account;
                if (a != null)
                {
                    var parameters = new AccountParameters(a.id);
                    this.Frame.Navigate(typeof(AccountDetailsPage), parameters);
                }
            }

        }
    }
}
