using SplitExpensesCalculation.Models;

namespace SplitExpensesCalculation.Core;

public interface IDebtCalculator
{
    public List<Debtor> Сalculate(Group _group);
}