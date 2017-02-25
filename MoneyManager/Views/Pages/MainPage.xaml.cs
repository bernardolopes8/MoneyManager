using MoneyManager.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;
using MoneyManager.Pages;
using MoneyManager.Views.Pages;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MoneyManager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public SettingsHandler SettingsHandler { get; set; }

        public object Header { get; set; }
        public object MainListBox { get; private set; }
        public object MyFrame { get; private set; }

        public MainPage()
        {
            this.InitializeComponent();
            this.SettingsHandler = new SettingsHandler();
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
            

            if(Debt_Click.IsSelected)
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
    }
  }
