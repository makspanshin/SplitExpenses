namespace SplitExpensesCalculation.Models;

public class Group
{
    public Group(string Name)
    {
        this.Name = Name;
        Members = new List<Member>();
        Transactions = new List<Transaction>();
    }

    public string Name { get; set; }

    public List<Member>? Members { get; set; }

    public List<Transaction>? Transactions { get; set; }
}