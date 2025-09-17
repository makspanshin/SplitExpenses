namespace SplitExpensesCalculation.Models;

public class Transaction
{
    public string Name { get; set; }

    public double Amount { get; set; }

    public Member Payer { get; set; }
}