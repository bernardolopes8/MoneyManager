using MoneyManager.ViewModel;
using System;

namespace MoneyManager.Converters
{
    public class AccountConverter : Windows.UI.Xaml.Data.IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            return AccountViewModel.getName((long)value);
        }
        
        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
