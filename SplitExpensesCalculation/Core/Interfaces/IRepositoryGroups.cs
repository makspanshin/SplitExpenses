using SplitExpensesCalculation.Models;

namespace SplitExpensesCalculation.Core.Interfaces;

public interface IRepositoryGroups
{
    public Task GetGroupAsync(string name, string? Nickname);

    public Task AddGroupAsync(string name);

    public Task AddMemberAsync(string name);

    public Task RemoveMemberAsync(Member member);

    public Task AddTransactionAsync(string? NameTran, double amount, string namePayer);

    public Task RemoveTransactionAsync(Transaction transaction);
}