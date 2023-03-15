using PocDDD.Api.Application.Expense.Ports.ServerSide;
using PocDDD.Api.Data;
using PocDDD.Api.Infrastructure.User;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace PocDDD.Api.Infrastructure.Expense;

public class ExpenseRepository : IExpenseRepository
{
    private readonly AppDbContext _appDbContext;

    public ExpenseRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<ImmutableArray<Domain.Expense.Expense>> Get(Guid userId)
    {
        var expenses = Enumerable.Empty<Domain.Expense.Expense>();

        var userWithExpenses = await _appDbContext.Users
            .Include(_ => _.Expenses)
            .FirstOrDefaultAsync(i => i.Id == userId);

        if (userWithExpenses != null)
        {
            var user = UserMapper.MapFromUserDto(userWithExpenses);
            expenses = userWithExpenses.Expenses.ConvertAll(ExpenseMapper.MapFromExpenseDto);
        }

        return expenses.ToImmutableArray();
    }

    public async Task Save(Guid userId, Domain.Expense.Expense expense)
    {
        var expenseDto = new ExpenseDto
        {
            Commentary = expense.Commentary,
            Currency = expense.Currency,
            From = expense.From,
            Mounth = expense.Mounth,
            Type = (ExpenseTypeDto)expense.Type,
            UserId = userId,
        };
        await _appDbContext.Expenses.AddAsync(expenseDto);
        await _appDbContext.SaveChangesAsync();
    }
}