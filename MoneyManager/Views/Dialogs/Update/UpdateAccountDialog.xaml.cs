using MoneyManager.ViewModel;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MoneyManager.Views.Dialogs.Update
{
    public sealed partial class UpdateAccountDialog : ContentDialog
    {
        public AccountViewModel AccountViewModel { get; set; }

        public UpdateAccountDialog(AccountViewModel upavm)
        {
            this.InitializeComponent();
            AccountViewModel = upavm;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            AccountViewModel.update();
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
