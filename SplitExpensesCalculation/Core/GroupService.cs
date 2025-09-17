using SplitExpensesCalculation.Core.Interfaces;
using SplitExpensesCalculation.Models;

namespace SplitExpensesCalculation.Core;

public class GroupService(IDebtCalculator debtCalculator)
{
    private IDebtCalculator DebtCalculator { get; } = debtCalculator;

    public void DebtCalculation(Group _group)
    {
        var debtor = DebtCalculator.Сalculate(_group);

        foreach (var member in debtor)
        foreach (var test in member.CreditorDictionary)
            Console.WriteLine($"{member.Name} paid {Math.Abs(test.Value):F2} to {test.Key.Name}");
    }
}