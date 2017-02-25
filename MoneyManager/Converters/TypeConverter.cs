using MoneyManager.ViewModel;
using System;

namespace MoneyManager.Converters
{
    public class TypeConverter : Windows.UI.Xaml.Data.IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            return TypeViewModel.getName((long)value);
        }
        
        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
