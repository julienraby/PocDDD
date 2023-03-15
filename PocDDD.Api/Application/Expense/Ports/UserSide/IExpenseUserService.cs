using PocDDD.Api.Domain.Expense;

namespace PocDDD.Api.Application.Expense.Ports.UserSide;

public interface IExpenseUserService
{
    Task<ExpensesUser> GetExpenses(Guid userId);

    Task Save(Guid userId, Domain.Expense.Expense expense);
}