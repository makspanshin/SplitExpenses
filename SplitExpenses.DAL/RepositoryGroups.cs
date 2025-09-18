using Microsoft.EntityFrameworkCore;
using Models;
using Newtonsoft.Json;
using SplitExpensesCalculation.Models;
using System.Text.Json.Nodes;
using Group = Models.Group;

namespace SplitExpenses.DAL;

public class RepositoryGroups : IRepositoryGroups, IDisposable
{
    private readonly SplitExpensesDbContext _dbContext;

    public RepositoryGroups(SplitExpensesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Dispose()
    {
        if (_dbContext is not null) _dbContext.Dispose();
    }

    public async Task AddMemberAsync(string nameMember, string? nickname, string nameGroup)
    {
        var currentUser = await _dbContext.UsersTgs.Include(usersTg => usersTg.Groups).FirstAsync(x => x.Nickname == nickname);

        if (currentUser is not null)
        {
            var curGroup = currentUser.Groups.First(x => x.Name == nameGroup);
            if (curGroup is not null)
            {
                var members = JsonConvert.DeserializeObject<List<Member>>(curGroup.Members);
                members?.Add(new Member(nameMember));
                curGroup.Members = JsonConvert.SerializeObject(members);
            }
        }

        //Пользователь не найден
        await _dbContext.SaveChangesAsync();
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

    public async Task AddGroupAsync(string name, string nickName)
    {
        var currentUser = await _dbContext.UsersTgs.FirstAsync(x => x.Nickname == nickName);

        if (currentUser is not null)
        {
            currentUser.Groups.Add(new Group { Name = name, Owner = currentUser });
        }

        //Пользователь не найден
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddUserAsync(string nickName)
    {
        var currentUser = await _dbContext.UsersTgs.FirstAsync(x => x.Nickname == nickName);

        if (currentUser is not null)
        {
            //Пользователь уже существует 
        }
        else
        {
            _dbContext.UsersTgs.Add(new UsersTg { Nickname = nickName });
        }

        await _dbContext.SaveChangesAsync();
    }
}