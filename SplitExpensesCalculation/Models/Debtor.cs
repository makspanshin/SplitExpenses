namespace SplitExpensesCalculation.Models;

public class Debtor : Member
{
    public Debtor(string name, double amount)
        : base(name)
    {
        Name = name;
        Amount = amount;
    }

    public double Amount { get; set; }

    public Dictionary<Member, double> CreditorDictionary { get; set; } = new();
}