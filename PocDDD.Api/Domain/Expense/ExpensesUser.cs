using System.Collections.Immutable;

namespace PocDDD.Api.Domain.Expense;

public sealed record ExpensesUser(string User, ImmutableArray<Expense> Expenses);