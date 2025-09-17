using SplitExpensesCalculation.Models;

namespace SplitExpensesCalculation.Core.Interfaces;

public interface IDebtCalculator
{
    public List<Debtor> Сalculate(Group _group);
}