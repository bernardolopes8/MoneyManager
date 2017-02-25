using MoneyManager_BL_DAL;
using System;
using System.Linq;

namespace MoneyManagerConsoleTestA
{
    class Program
    {
        static void Main(string[] args)
        {

            // TYPE --------------------------------------------------------------
            MType.CreateTable();

            MType outgoing = new MType();
            outgoing.name = "Outgoing";
            outgoing.Create();

            MType incoming = new MType();
            incoming.name = "Incoming";
            incoming.Create();
            
            foreach (MType type in MType.RetrieveAll())
            {
                Console.WriteLine(type.id + " - " + type.name);
            }


            //CATEGORY --------------------------------------------------------------
            Category.CreateTable();

            Category food = new Category();
            food.name = "Food";
            food.Create();

            Category bills = new Category();
            bills.name = "Bills";
            bills.Create();

            Category loans = new Category();
            loans.name = "Loans";
            loans.Create();

            Category vehicles = new Category();
            vehicles.name = "Vehicles";
            vehicles.Create();

            Category psales = new Category();
            psales.name = "Personal sales";
            psales.Create();

            foreach (Category category in Category.RetrieveAll())
            {
                Console.WriteLine(category.id + " - " + category.name);
            }


            // CURRENCY --------------------------------------------------------------
            Currency.CreateTable();

            Currency euro = new Currency();
            euro.name = "Euro";
            euro.Create();

            Currency usDollar = new Currency();
            usDollar.name = "US Dollar";
            usDollar.Create();

            foreach (Currency currency in Currency.RetrieveAll())
            {
                Console.WriteLine(currency.id + " - " + currency.name);
            }

            //Currency test = Currency.RetrieveById(2).First();
            //test.Delete();

            //Currency test = new Currency();
            //test.id = Currency.RetrieveByName("US Dollar").First().id;
            //test.name = "American Dollar";
            //test.value = 0.7;
            //test.Update();


            // USER --------------------------------------------------------------
            User.CreateTable();

            User bernardo = new User();
            bernardo.login = "bernardo";
            bernardo.password = "benfica";
            bernardo.currency_id = 1;
            bernardo.Create();

            User tiago = new User();
            tiago.login = "tiago";
            tiago.password = "benfica2";
            tiago.currency_id = 1;
            tiago.Create();

            foreach (User user in User.RetrieveAll())
            {
                Console.WriteLine(user.id + " - " + user.login);
                Console.WriteLine("PW - " + user.password);
            }


            // ACCOUNT --------------------------------------------------------------
            Account.CreateTable();

            Account wallet = new Account();
            wallet.name = "Wallet";
            wallet.balance = 0;
            wallet.user_id = User.RetrieveByLogin("bernardo").First().id;
            wallet.Create();

            Account bank_acc = new Account();
            bank_acc.name = "Bank account";
            bank_acc.balance = 702.01;
            bank_acc.user_id = User.RetrieveByLogin("bernardo").First().id;
            bank_acc.Create();

            foreach (Account account in Account.RetrieveAll())
            {
                Console.WriteLine(account.id + " - " + account.name);
                Console.WriteLine("Balance - " + account.balance + " " + Currency.RetrieveById(User.RetrieveById(account.user_id).First().currency_id).First().name);
                Console.WriteLine("Owner - " + User.RetrieveById(account.user_id).First().login);
            }


            // DEBT --------------------------------------------------------------
            Debt.CreateTable();

            Debt loan = new Debt();
            loan.amount = 1500;
            loan.description = "Bank loan";
            loan.deadline = DateTime.Parse("11/12/2016");
            loan.category_id = Category.RetrieveByName("Loans").First().id;
            loan.user_id = User.RetrieveByLogin("tiago").First().id;
            loan.type_id = MType.RetrieveByName("Outgoing").First().id;
            loan.Create();

            Debt library = new Debt();
            library.amount = 5.8;
            library.description = "Library membership";
            library.deadline = DateTime.Parse("30/11/2016");
            library.category_id = Category.RetrieveByName("Bills").First().id;
            library.user_id = User.RetrieveByLogin("tiago").First().id;
            library.type_id = MType.RetrieveByName("Outgoing").First().id;
            library.Create();

            foreach (Debt debt in Debt.RetrieveAll())
            {
                Console.WriteLine(debt.description + " - " + debt.amount);
            }
            
            //foreach (Debt debt in Debt.RetrieveByDeadline(DateTime.Parse("30/11/2016")))
            //{
            //    Console.WriteLine(debt.description + " - " + debt.amount);
            //}

            //Debt test = new Debt();
            //test = Debt.RetrieveByUser(2).First();
            //test.description = "Test worked";
            //test.amount = 700;
            //test.Update();


            // TRANSACTION --------------------------------------------------------------

            Transaction.CreateTable();

            Transaction car = new Transaction();
            car.amount = 15000;
            car.description = "Car purchase";
            car.date = DateTime.Parse("25/08/2016");
            car.category_id = Category.RetrieveByName("Vehicles").First().id;
            car.user_id = User.RetrieveByLogin("bernardo").First().id;
            car.type_id = MType.RetrieveByName("Outgoing").First().id;
            car.account_id = Account.RetrieveByName("Bank account").First().id;
            car.Create();

            Transaction sale = new Transaction();
            sale.amount = 145;
            sale.description = "Used TV sale";
            sale.date = DateTime.Parse("12/01/2014");
            sale.category_id = Category.RetrieveByName("Personal sales").First().id;
            sale.user_id = User.RetrieveByLogin("bernardo").First().id;
            sale.type_id = MType.RetrieveByName("Incoming").First().id;
            sale.account_id = Account.RetrieveByName("Wallet").First().id;
            sale.Create();

            foreach (Transaction t in Transaction.RetrieveAll())
            {
                Console.WriteLine(t.description + " - " + t.amount);
            }

            //foreach (Transaction t in Transaction.RetrieveByDateRange(DateTime.Parse("1/1/2013"), DateTime.Parse("31/12/2015")))
            //{
            //    Console.WriteLine(t.description + " - " + t.amount);
            //}
        }
    }
}
