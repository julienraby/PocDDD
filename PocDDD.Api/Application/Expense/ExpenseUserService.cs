using PocDDD.Api.Application.Expense.Ports.ServerSide;
using PocDDD.Api.Application.Expense.Ports.UserSide;
using PocDDD.Api.Application.User.Ports.ServerSide;
using PocDDD.Api.Domain.Expense;
using System.Collections.Immutable;

namespace PocDDD.Api.Application.Expense;

public sealed class ExpenseUserService : IExpenseUserService
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IUserRepository _userRepository;

    public ExpenseUserService(IExpenseRepository expenseRepository, IUserRepository userRepository)
    {
        _expenseRepository = expenseRepository;
        _userRepository = userRepository;
    }

    public async Task<ExpensesUser> GetExpenses(Guid userId)
    {
        var user = await _userRepository.Get(userId);
        if (user != null)
        {
            var expenses = await _expenseRepository.Get(userId);
            return new ExpensesUser(user.ToString(), expenses);
        }
        else
        {
            return null;
        }
    }

    public async Task Save(Guid userId, Domain.Expense.Expense expense)
    {
        await _expenseRepository.Save(userId, expense);
    }
}