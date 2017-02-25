using System.Collections.Generic;

namespace MoneyManager
{
    public class SettingsHandler
    {
        public Dictionary<string, string> Currencies { get; set; }
        public Windows.Storage.ApplicationDataContainer localSettings { get; set; }
        public string SelectedCurrency { get; set; }

        public SettingsHandler()
        {
            localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            Currencies = new Dictionary<string, string> {
            { "EUR", "Euro" }, { "GBP", "British Pound Sterling" }, { "USD", "US Dollar" } };

            GetSelectedCurrency();
        }
        
        public string GetSelectedCurrency()
        {
            if (localSettings.Values.ContainsKey("selectedCurrency"))
            {
                SelectedCurrency = localSettings.Values["selectedCurrency"].ToString();
            }
            else
            {
                SelectedCurrency = "EUR";
            }

            return SelectedCurrency;
        }
            
    }
}
