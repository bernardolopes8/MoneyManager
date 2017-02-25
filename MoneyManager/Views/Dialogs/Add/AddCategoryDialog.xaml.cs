using MoneyManager.ViewModel;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MoneyManager.Views.Dialogs.Add
{
    public sealed partial class AddCategoryDialog : ContentDialog
    {
        public CategoryViewModel CategoryViewModel { get; set; }

        public AddCategoryDialog(CategoryViewModel upcvm)
        {
            this.InitializeComponent();
            CategoryViewModel = upcvm;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            CategoryViewModel.Add();
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
