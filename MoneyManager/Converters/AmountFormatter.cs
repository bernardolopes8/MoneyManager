using System;

namespace MoneyManager.Converters
{
    public class AmountFormatter : Windows.UI.Xaml.Data.IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            return value.ToString() + " " + (new SettingsHandler()).SelectedCurrency;
        }
        
        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
