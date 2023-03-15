namespace PocDDD.Api.Infrastructure.Expense;

public sealed class ExpenseMapper
{
    public static Domain.Expense.Expense MapFromExpenseDto(ExpenseDto expenseDto)
    {
        return new Domain.Expense.Expense(expenseDto.Id,
            expenseDto.From,
            ExpenseTypeMapper.MapFromExpenseDto(expenseDto.Type),
            expenseDto.Mounth,
            expenseDto.Currency,
            expenseDto.Commentary);
    }
}
