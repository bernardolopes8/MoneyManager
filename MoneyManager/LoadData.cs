using MoneyManager_BL_DAL;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.UI.Notifications;

namespace MoneyManager
{
    static class LoadData
    {
        public static void LoadInitialData()
        {
            CreateDefaultAccount();
            LoadTypes();
            LoadCategories();
        }

        public static async Task<XDocument> LoadFromXML(string XMLFile)
        {
            StorageFolder InstallationFolder = Package.Current.InstalledLocation;
            StorageFile file = await InstallationFolder.GetFileAsync(XMLFile);
            Stream Data = await file.OpenStreamForReadAsync();
            XDocument LoadedData = XDocument.Load(Data);
            return LoadedData;
        }

        private static void CreateDefaultAccount()
        {
            ViewModel.AccountViewModel avm = new ViewModel.AccountViewModel();
            if (Account.RetrieveAll().Count() == 0)
            {
                avm.account = new Account();
                avm.account.name = "Wallet";
                avm.account.balance = 0;
                avm.Add();
            }
        }

        public static async void LoadTypes()
        {
            string XMLFile = @"Assets\types.xml";
            XDocument LoadedTypes = await LoadFromXML(XMLFile);

            var data = from query in LoadedTypes.Descendants("item")
                       select new MType
                       {
                           name = (string)query.Element("name")
                       };

            foreach (var item in data)
            {
                item.Create();
            }
        }

        public static async void LoadCategories()
        {
            string XMLFile = @"Assets\categories.xml";
            XDocument LoadedCategories = await LoadFromXML(XMLFile);

            var data = from query in LoadedCategories.Descendants("item")
                       select new Category
                       {
                           name = (string)query.Element("name")
                       };

            foreach (var item in data)
            {
                item.Create();
            }
        }

        public static void ShowToastNotification(string title, string stringContent)
        {
            ToastNotifier ToastNotifier = ToastNotificationManager.CreateToastNotifier();
            Windows.Data.Xml.Dom.XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
            Windows.Data.Xml.Dom.XmlNodeList toastNodeList = toastXml.GetElementsByTagName("text");
            toastNodeList.Item(0).AppendChild(toastXml.CreateTextNode(title));
            toastNodeList.Item(1).AppendChild(toastXml.CreateTextNode(stringContent));
            Windows.Data.Xml.Dom.IXmlNode toastNode = toastXml.SelectSingleNode("/toast");
            //Windows.Data.Xml.Dom.XmlElement audio = toastXml.CreateElement("audio");
            //audio.SetAttribute("src", "ms-winsoundevent:Notification.SMS");

            ToastNotification toast = new ToastNotification(toastXml);
            toast.ExpirationTime = DateTime.Now.AddSeconds(1);
            ToastNotifier.Show(toast);
        }
    }
}
