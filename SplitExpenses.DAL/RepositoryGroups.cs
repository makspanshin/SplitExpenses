using Models;
using SplitExpensesCalculation.Core.Interfaces;
using SplitExpensesCalculation.Models;

namespace SplitExpenses.DAL
{
    public class RepositoryGroups : IRepositoryGroups
    {
        private readonly SplitExpensesDbContext _dbContext;

        public RepositoryGroups(SplitExpensesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddGroupAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task AddMemberAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task AddTransactionAsync(string? NameTran, double amount, string namePayer)
        {
            throw new NotImplementedException();
        }

        public Task GetGroupAsync(string name, string? Nickname)
        {
            throw new NotImplementedException();
        }

        public Task RemoveMemberAsync(Member member)
        {
            throw new NotImplementedException();
        }

        public Task RemoveTransactionAsync(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
