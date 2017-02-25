using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using MoneyManager.ViewModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MoneyManager.Views.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AccountDetailsPage : Page
    {
        public TransactionViewModel TransactionViewModel { get; set; }

        public AccountDetailsPage()
        {
            this.InitializeComponent();
            this.TransactionViewModel = new TransactionViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
    {
            var parameters = e.Parameter as AccountParameters;
            TransactionViewModel.SetAccountTransactions(parameters.account_id);

            var currentView = SystemNavigationManager.GetForCurrentView();

        currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

        currentView.BackRequested += backButton_Tapped;
    }

    private void backButton_Tapped(object sender, BackRequestedEventArgs e)

    {

        if (Frame.CanGoBack) Frame.GoBack();

    }

    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
        base.OnNavigatedFrom(e);

        var currentView = SystemNavigationManager.GetForCurrentView();

        currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

        currentView.BackRequested -= backButton_Tapped;
    }

    private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
