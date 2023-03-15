namespace PocDDD.Api.Domain.Expense;

public sealed record Expense(Guid Id, DateTime From, ExpenseType Type, decimal Mounth, string Currency, string Commentary);
