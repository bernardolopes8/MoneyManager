using MoneyManager_BL_DAL;
using System.Collections.ObjectModel;
using System.Linq;

namespace MoneyManager.ViewModel
{
    public class CategoryViewModel
    {
        public ObservableCollection<Category> Categories { get; set; }

        public Category category { get; set; }

        public CategoryViewModel()
        {
            Categories = Category.RetrieveAll();
        }

        internal void Add()
        {
            category.Create();
            Categories.Add(category);
        }

        internal void update()
        {
            Categories.Remove(category);
            category.Update();
            Categories.Add(category);
        }

        internal void delete()
        {
            Categories.Remove(category);
            category.Delete();
        }

        public static string getName(long category_id)
        {
            return Category.RetrieveById(category_id).First().name;
        }
    }
}
