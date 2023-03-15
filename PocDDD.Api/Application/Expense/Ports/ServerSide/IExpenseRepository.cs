using System.Collections.Immutable;

namespace PocDDD.Api.Application.Expense.Ports.ServerSide;

public interface IExpenseRepository
{
    Task<ImmutableArray<Domain.Expense.Expense>> Get(Guid userId);

    Task Save(Guid userId, Domain.Expense.Expense expense);
}