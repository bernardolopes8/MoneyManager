using MoneyManager_BL_DAL;
using System.Collections.ObjectModel;

namespace MoneyManager.ViewModel
{
    public class DebtViewModel
    {
        public ObservableCollection<Debt> IncomeDebts { get; set; }
        public ObservableCollection<Debt> ExpenseDebts { get; set; }

        public Debt debt { get; set; }

        public DebtViewModel()
        {
            IncomeDebts = Debt.RetrieveByType(TypeViewModel.RetrieveTypeId("Income"));
            ExpenseDebts = Debt.RetrieveByType(TypeViewModel.RetrieveTypeId("Expense"));
        }

        private void CollectionAdd()
        {
            if (debt.type_id == TypeViewModel.RetrieveTypeId("Income"))
            {
                IncomeDebts.Add(debt);
            }
            else
            {
                ExpenseDebts.Add(debt);
            }
        }

        private void CollectionRemove()
        {
            if (IncomeDebts.Contains(debt))
            {
                IncomeDebts.Remove(debt);
            }
            else
            {
                ExpenseDebts.Remove(debt);
            }
        }

        internal void Add()
        {
           debt.Create();
           CollectionAdd();
        }

        internal void Update()
        {
            CollectionRemove();
            debt.Update();
            CollectionAdd();
        }

        internal void Delete()
        {
            CollectionRemove();
            debt.Delete();
        }
    }
}
