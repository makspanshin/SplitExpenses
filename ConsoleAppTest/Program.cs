

using SplitExpensesCalculation.Models;
using SplitExpensesCalculation.Core;
namespace ConsoleAppTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Group testGroup = new Group("Командировка");

            GroupService test = new GroupService(new DebtCalculator());

            testGroup.Members.Add(new Member("Maks"));

            testGroup.Members.Add(new Member("Dima"));

            testGroup.Members.Add(new Member("Timur"));

            testGroup.Members.Add(new Member("Tolya"));

            testGroup.Transactions.Add(new Transaction(){ Name = "Пиво",Amount = 1000, Payer = new Member("Dima")});

            test.DebtCalculation(testGroup);
        }
    }
}
