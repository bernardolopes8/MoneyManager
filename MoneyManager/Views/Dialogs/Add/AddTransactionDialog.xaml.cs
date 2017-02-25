using MoneyManager.ViewModel;
using MoneyManager_BL_DAL;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MoneyManager.Views.Dialogs.Add
{
    public sealed partial class AddTransactionDialog : ContentDialog
    {

        public TransactionViewModel TransactionViewModel { get; set; }

        public CategoryViewModel CategoryViewModel { get; set; }

        public AccountViewModel  AccountViewModel { get; set; }

        public ObservableCollection<Category> categories = Category.RetrieveAll();

        public ObservableCollection<Account> accounts = Account.RetrieveAll();

        public ObservableCollection<MType> type = MType.RetrieveAll();

        public AddTransactionDialog()
        {
            this.InitializeComponent();
            TransactionViewModel = new TransactionViewModel();
        }

        public AddTransactionDialog(TransactionViewModel tvm)
        {
            this.InitializeComponent();
            TransactionViewModel = tvm;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            int year = date.Date.Year, month = date.Date.Month, day = date.Date.Day, hour = time.Time.Hours, min = time.Time.Minutes, sec = time.Time.Seconds;
            TransactionViewModel.transaction.date = new DateTime(year, month, day, hour, min, sec);
            TransactionViewModel.Add();
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            long selectedValue = (long)cmb.SelectedValue;
            TransactionViewModel.transaction.category_id = selectedValue;
        }

        private void ComboBox_SelectionChanged2(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            long selectedValue = (long)cmb.SelectedValue;
            TransactionViewModel.transaction.type_id = selectedValue;
        }

        private void ComboBox_SelectionChanged3(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            long selectedValue = (long)cmb.SelectedValue;
            TransactionViewModel.transaction.account_id = selectedValue;
        }
    }

}
