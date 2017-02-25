using MoneyManager.ViewModel;
using MoneyManager_BL_DAL;
using System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using MoneyManager.Views.Dialogs.Add;
using MoneyManager.Views.Dialogs.Update;
using Windows.UI.Popups;
using MoneyManager.Views.Pages;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MoneyManager.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TransactionsPage : Page
    {
        public SettingsHandler SettingsHandler { get; set; }
        public TransactionViewModel TransactionViewModel { get; set; }

        public object Header { get; set; }
        public object MainListBox { get; private set; }
        public object MyFrame { get; private set; }

        public TransactionsPage()
        {
            this.InitializeComponent();
            this.SettingsHandler = new SettingsHandler();
            this.TransactionViewModel = new TransactionViewModel();
        }

        private void HomeRadioButton_Click(object sender, RoutedEventArgs e)
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
            TransactionViewModel.transaction = new Transaction();
            AddTransactionDialog md = new AddTransactionDialog(TransactionViewModel);
            MessageDialog a = new MessageDialog("Transaction Created!");

            await md.ShowAsync();
           
        }

        private async void Update_Click(Object sender, RoutedEventArgs e)
        {
            FrameworkElement fe = sender as FrameworkElement;
            if (fe != null)
            {
                Transaction t = fe.DataContext as Transaction;
                if (t != null)
                {
                    TransactionViewModel.transaction = t;
                    UpdateTransactionDialog tm = new UpdateTransactionDialog(TransactionViewModel);
                    await tm.ShowAsync();
                }
            }
        }

        private async void Delete_Click(Object sender, RoutedEventArgs e)
        {
            FrameworkElement fe = sender as FrameworkElement;
            if (fe != null)
            {
                Transaction t = fe.DataContext as Transaction;
                if (t != null)
                {
                    TransactionViewModel.transaction = t;
                    TransactionViewModel.Delete();
                    MessageDialog md = new MessageDialog("Transaction Deleted!");
                    await md.ShowAsync();
                }
            }
        }
    }
}
