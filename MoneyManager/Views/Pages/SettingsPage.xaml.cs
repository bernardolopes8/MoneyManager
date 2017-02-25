using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MoneyManager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        SettingsHandler SettingsHandler;
        private object myMediaElement;

        public SettingsPage()
        {
            this.InitializeComponent();
            SettingsHandler = new SettingsHandler();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
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

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void CurrencyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SaveCurrencySetting(string key)
        {
            SettingsHandler.localSettings.Values["selectedCurrency"] = key;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string selectedValue = (string)CurrencyComboBox.SelectedValue;
            SaveCurrencySetting(selectedValue);
            LoadData.ShowToastNotification("Money Manager", selectedValue + " selected as current currency!");
        }
    }
}
