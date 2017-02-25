using MoneyManager.ViewModel;
using System;

namespace MoneyManager.Converters
{
    public class CategoryConverter : Windows.UI.Xaml.Data.IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            return CategoryViewModel.getName((long)value);
        }
        
        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
