using MoneyManager.ViewModel;
using MoneyManager.Views.Dialogs.Update;
using MoneyManager.Views.Pages;
using MoneyManager_BL_DAL;
using System;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MoneyManager.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DebtsPage : Page
    {
        public SettingsHandler SettingsHandler { get; set; }
        public DebtViewModel DebtViewModel { get; set; }
        

        public object Header { get; set; }
        public object MainListBox { get; private set; }
        public object MyFrame { get; private set; }

        public DebtsPage()

        {
            this.InitializeComponent();
            this.SettingsHandler = new SettingsHandler();
            this.DebtViewModel = new DebtViewModel();
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
            DebtViewModel.debt = new Debt();
            AddDebtDialog md = new AddDebtDialog(DebtViewModel);
            await md.ShowAsync();
        }

        private async void Update_Click(object sender, RoutedEventArgs e)
        {

            FrameworkElement fe = sender as FrameworkElement;
            if (fe != null)
            {
                Debt d = fe.DataContext as Debt;
                if (d != null)
                {
                    DebtViewModel.debt = d;
                    UpdateDebt dm = new UpdateDebt(DebtViewModel);
                    await dm.ShowAsync();
                }
            }
        
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        { 

            FrameworkElement fe = sender as FrameworkElement;
            if (fe != null)
            {
                Debt d = fe.DataContext as Debt;
                if (d != null)
                {
                    DebtViewModel.debt = d;
                    DebtViewModel.Delete();
                    MessageDialog md = new MessageDialog("Debt Deleted!");
                    await md.ShowAsync();
                }
            }
        }
    }
  }

 