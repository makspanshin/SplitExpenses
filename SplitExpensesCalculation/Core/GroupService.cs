using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
using SplitExpensesCalculation.Models;
using System.Linq;

namespace SplitExpensesCalculation.Core;

internal class GroupService(Group group, IDebtCalculator debtCalculator)
{
    private readonly Group _group = group;
    private IDebtCalculator DebtCalculator { get; } = debtCalculator;

    public void AddMember(string name)
    {
        if (name != null)
            _group.Members.Add(new Member { Name = name });
    }

    public void RemoveMember(Member member)
    {
        _group.Members.Remove(member);
    }

    public double GetTotalAmountMember(string Name)
    {
        var trans = _group.Transactions.FindAll(x => x.Payer.Name == Name);

        return trans.Sum(x => x.Amount);
    }

    public Dictionary<Member, double> GetTotalAmountMembers()
    {
        return _group.Members.ToDictionary(member => member, member => GetTotalAmountMember(member.Name));
    }

    public void AddTransaction(string? NameTran, double amount, string namePayer)
    {
        var payer = _group.Members.Find(x => x.Name == namePayer);

        if (payer != null) _group.Transactions.Add(new Transaction { Name = NameTran, Amount = amount, Payer = payer });
    }

    
}