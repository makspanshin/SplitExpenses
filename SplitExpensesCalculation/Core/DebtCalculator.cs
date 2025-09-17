using SplitExpensesCalculation.Core.Interfaces;
using SplitExpensesCalculation.Models;

namespace SplitExpensesCalculation.Core;

public class DebtCalculator : IDebtCalculator
{
    public List<Debtor> Сalculate(Group _group)
    {
        var debts = DebtCalculationInGroup(_group);

        var debtorsList = FindDebtors(debts);

        // Сортируем список должников по возрастанию суммы (от наибольшего долга к наименьшему)
        debtorsList = debtorsList.OrderBy(o => o.Amount).ToList();

        // Процесс минимизации транзакций
        foreach (var owedPerson in debtorsList)
            // Пока должник не рассчитался (его баланс не равен 0)
            while (debts[owedPerson] != 0)
            {
                var negAmt = debts[owedPerson]; // Сумма, которую должны этому человеку (отрицательная)

                // Находим лучшего человека для расчетов (кто должен больше всего)
                var bestPayer = GetBestMatch(negAmt, debts);
                var posAmt = debts[bestPayer]; // Сумма, которую должен лучший плательщик (положительная)

                // Сценарий 1: Плательщик должен больше или столько же, сколько должны должнику
                if (posAmt >= Math.Abs(negAmt))
                {
                    debts[owedPerson] = 0.0; // Должник получил все
                    debts[bestPayer] -= Math.Abs(negAmt); // Плательщик выплатил часть своего долга

                    debtorsList.Find(x => x == bestPayer)?.CreditorDictionary.Add(owedPerson, negAmt);

                    Console.WriteLine($"{bestPayer.Name} paid {posAmt:F2} to {owedPerson.Name}");
                }
                // Сценарий 2: Плательщик должен меньше, чем должны должнику
                else
                {
                    // Должник получил часть, ему все еще должны
                    debts[owedPerson] += posAmt; // negAmt станет менее отрицательным
                    debts[bestPayer] = 0.0; // Плательщик полностью рассчитался

                    Console.WriteLine($"{bestPayer.Name} paid {posAmt:F2} to {owedPerson.Name}");
                }
            }

        return debtorsList;
    }

    private static Dictionary<Member, double> DebtCalculationInGroup(Group _group)
    {
        var owes = new Dictionary<Member, double>();

        foreach (var e in _group.Transactions)
        {
            var owedAmtPerConsumer = e.Amount / _group.Members.Count;

            foreach (var c in _group.Members)
                // Если потребитель не является плательщиком, он должен свою долю
                if (!e.Payer.Equals(c))
                {
                    // Добавляем долю к сумме, которую должен потребитель 'c'
                    if (!owes.TryAdd(c, owedAmtPerConsumer)) owes[c] += owedAmtPerConsumer;

                    // Вычитаем долю из суммы, которую заплатил плательщик 'e.Spender'
                    // (т.е. увеличиваем его "кредит")
                    if (owes.ContainsKey(e.Payer))
                        owes[e.Payer] -= owedAmtPerConsumer; // Уменьшаем, потому что ему должны
                    else
                        owes.Add(e.Payer, -owedAmtPerConsumer);
                }
        }

        return owes;
    }

    private static List<Debtor> FindDebtors(Dictionary<Member, double> owes)
    {
        // Создаем список должников (тех, кто имеет отрицательный баланс - кому должны)
        var debtorsList = (from entry in owes where entry.Value < 0 select new Debtor(entry.Key.Name, entry.Value))
            .ToList();
        return debtorsList;
    }


    // Вспомогательный метод для поиска лучшего плательщика
    private static Member GetBestMatch(double negAmount, Dictionary<Member, double> owes)
    {
        Member greatestS = null;
        double greatestAmt = -1; // Инициализируем отрицательным значением, чтобы любой положительный баланс был больше

        foreach (var entry in owes)
        {
            var s = entry.Key;
            var amt = entry.Value;

            if (amt > 0) // Ищем только тех, кто должен (положительный баланс)
            {
                // Если нашли того, кто должен ровно столько, сколько нужно - это идеальный вариант
                if (amt == Math.Abs(negAmount)) return s;
                // Иначе, ищем того, кто должен больше всего (но не больше, чем нужно, если есть точный матч)

                if (greatestS == null || amt > greatestAmt)
                {
                    greatestAmt = amt;
                    greatestS = s;
                }
            }
        }

        return greatestS;
    }
}