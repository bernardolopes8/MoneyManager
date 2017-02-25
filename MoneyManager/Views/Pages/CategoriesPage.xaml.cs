using MoneyManager.Pages;
using MoneyManager.ViewModel;
using MoneyManager.Views.Dialogs.Add;
using MoneyManager.Views.Dialogs.Update;
using MoneyManager_BL_DAL;
using System;
using System.Linq;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MoneyManager.Views.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CategoriesPage : Page
    {
        public CategoryViewModel CategoryViewModel { get; set; }
        public TransactionViewModel TransactionViewModel { get; set; }
        public SettingsHandler SettingsHandler { get; set; }

        public CategoriesPage()
        {
            InitializeComponent();
            SettingsHandler = new SettingsHandler();
            CategoryViewModel = new CategoryViewModel();
            TransactionViewModel = new TransactionViewModel();
            TransactionViewModel.Transactions = Transaction.RetrieveAll();

            var groups = from t in TransactionViewModel.Transactions
                         group t by t.category_id;

            cvs.Source = groups;
            
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
            var currentView = SystemNavigationManager.GetForCurrentView();

            currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

        }

        private async void Add_Click(object sender, RoutedEventArgs e)
        {

            CategoryViewModel.category = new Category();
            AddCategoryDialog md = new AddCategoryDialog(CategoryViewModel);
            await md.ShowAsync();

        }

        private async void Update_Click(Object sender, RoutedEventArgs e)
        {
            FrameworkElement fe = sender as FrameworkElement;
            if (fe != null)
            {
                Category c = fe.DataContext as Category;
                if (c != null)
                {
                    CategoryViewModel.category = c;
                    UpdateCategoryDialog cm = new UpdateCategoryDialog(CategoryViewModel);
                    await cm.ShowAsync();
                }
            }
        }

        private async void Delete_Click(Object sender, RoutedEventArgs e)
        {

            FrameworkElement fe = sender as FrameworkElement;
            if (fe != null)
            {
                Category cd = fe.DataContext as Category;
                if (cd != null)
                {
                    if (Transaction.RetrieveByCategory(cd.id).Count() != 0 || Debt.RetrieveByCategory(cd.id).Count() != 0)
                    {
                        MessageDialog md = new MessageDialog("Category can't be deleted because there are transactions or debts associated!");
                        await md.ShowAsync();
                    }
                    else if (CategoryViewModel.Categories.Count() == 1)
                    {
                        MessageDialog md = new MessageDialog("Invalid operation. There must be at least one category!");
                        await md.ShowAsync();
                    }
                    else {
                        CategoryViewModel.category = cd;
                        CategoryViewModel.delete();
                    }
                }
            }
        }
    }
    }