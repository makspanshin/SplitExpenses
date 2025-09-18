using SplitExpensesCalculation.Models;

namespace SplitExpenses.DAL;

public interface IRepositoryGroups
{
    public Task GetGroupAsync(string name, string? Nickname);

    public Task AddGroupAsync(string name, string nickName);

    public Task AddMemberAsync(string nameMember, string? nickname, string nameGroup);

    public Task RemoveMemberAsync(Member member);

    public Task AddTransactionAsync(string? NameTran, double amount, string namePayer);

    public Task RemoveTransactionAsync(Transaction transaction);
}