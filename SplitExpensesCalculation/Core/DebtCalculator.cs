using SplitExpensesCalculation.Models;
using System.Linq;
namespace SplitExpensesCalculation.Core;

public class DebtCalculator : IDebtCalculator
{
    public List<Debtor> Сalculate(Group _group)
    {

        var owes = new Dictionary<string, double>();

        foreach (var e in _group.Transactions)
        {
            var owedAmtPerConsumer = e.Amount / _group.Members.Count;

            foreach (var c in _group.Members)
            {
                // Если потребитель не является плательщиком, он должен свою долю
                if (!e.Payer.Equals(c))
                {
                    // Добавляем долю к сумме, которую должен потребитель 'c'
                    if (!owes.TryAdd(c.Name, owedAmtPerConsumer))
                    {
                        owes[c.Name] += owedAmtPerConsumer;
                    }

                    // Вычитаем долю из суммы, которую заплатил плательщик 'e.Spender'
                    // (т.е. увеличиваем его "кредит")
                    if (owes.ContainsKey(e.Payer.Name))
                    {
                        owes[e.Payer.Name] -= owedAmtPerConsumer; // Уменьшаем, потому что ему должны
                    }
                    else
                    {
                        owes.Add(e.Payer.Name, -owedAmtPerConsumer);
                    }
                }
            }
        }

        // Создаем список должников (тех, кто имеет отрицательный баланс - кому должны)
        var debtorsList = (from entry in owes where entry.Value < 0 select new Debtor(entry.Key, entry.Value)).ToList();

        // Сортируем список должников по возрастанию суммы (от наибольшего долга к наименьшему)
        debtorsList = debtorsList.OrderBy(o => o.Amount).ToList();

        // Процесс минимизации транзакций
        foreach (var owedPerson in debtorsList)
        {
            // Пока должник не рассчитался (его баланс не равен 0)
            while (owes[owedPerson.Name] != 0)
            {
                var negAmt = owes[owedPerson.Name]; // Сумма, которую должны этому человеку (отрицательная)

                // Находим лучшего человека для расчетов (кто должен больше всего)
                var bestPayer = GetBestMatch(negAmt, owes);
                var posAmt = owes[bestPayer]; // Сумма, которую должен лучший плательщик (положительная)

                // Сценарий 1: Плательщик должен больше или столько же, сколько должны должнику
                if (posAmt >= Math.Abs(negAmt)) 
                {
                    owes[owedPerson.Name] = 0.0; // Должник получил все
                    owes[bestPayer] -= Math.Abs(negAmt); // Плательщик выплатил часть своего долга


                    Console.WriteLine($"{bestPayer} paid {Math.Abs(negAmt):F2} to {owedPerson.Name}");
                }
                // Сценарий 2: Плательщик должен меньше, чем должны должнику
                else
                {
                    // Должник получил часть, ему все еще должны
                    owes[owedPerson.Name] += posAmt; // negAmt станет менее отрицательным
                    owes[bestPayer] = 0.0; // Плательщик полностью рассчитался

                    Console.WriteLine($"{bestPayer} paid {posAmt:F2} to {owedPerson.Name}");
                }
            }
        }

    }
    

    // Вспомогательный метод для поиска лучшего плательщика
    private static string GetBestMatch(double negAmount, Dictionary<string, double> owes)
    {
        string greatestS = null;
        double greatestAmt = -1; // Инициализируем отрицательным значением, чтобы любой положительный баланс был больше

        foreach (var entry in owes)
        {
            string s = entry.Key;
            double amt = entry.Value;

            if (amt > 0) // Ищем только тех, кто должен (положительный баланс)
            {
                // Если нашли того, кто должен ровно столько, сколько нужно - это идеальный вариант
                if (amt == Math.Abs(negAmount))
                {
                    return s;
                }
                // Иначе, ищем того, кто должен больше всего (но не больше, чем нужно, если есть точный матч)
                else if (greatestS == null || amt > greatestAmt)
                {
                    greatestAmt = amt;
                    greatestS = s;
                }
            }
        }
        return greatestS;
    }
}