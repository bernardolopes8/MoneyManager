using MoneyManager.ViewModel;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MoneyManager.Views.Dialogs.Add
{

    public sealed partial class AddAccountDialog : ContentDialog
    {
        public AccountViewModel AccountViewModel { get; set; }

        public AddAccountDialog(AccountViewModel avm)
        {
            this.InitializeComponent();
            AccountViewModel = avm;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            AccountViewModel.Add();
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }

    }
}
