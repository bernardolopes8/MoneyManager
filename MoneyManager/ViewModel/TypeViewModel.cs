using MoneyManager_BL_DAL;
using System.Collections.ObjectModel;
using System.Linq;

namespace MoneyManager.ViewModel
{
    public class TypeViewModel
    {
        public ObservableCollection<MType> Types { get; set; }

        public MType type { get; set; }

        public TypeViewModel()
        {
            Types = MType.RetrieveAll();
        }

        internal void Add()
        {
            type.Create();
            Types.Add(type);
        }

        public static long RetrieveTypeId(string name)
        {
           return MType.RetrieveByName(name).First().id;
        }

        public static string getName(long type_id)
        {
            return MType.RetrieveById(type_id).First().name;
        }

    }
}