namespace SplitExpensesCalculation.Models;

public class Debtor : Member
{
    public Debtor(string name, double amount)
    {
        Name = name;
        Amount = amount;
    }

    public double Amount { get; set; }
}