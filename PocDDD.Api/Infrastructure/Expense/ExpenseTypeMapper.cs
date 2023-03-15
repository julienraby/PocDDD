
using PocDDD.Api.Domain.Expense;

namespace PocDDD.Api.Infrastructure.Expense;

public sealed class ExpenseTypeMapper
{
    public static ExpenseType MapFromExpenseDto(ExpenseTypeDto expenseTypeDto)
    {
        //ToDo: rajouter un code pour les types et faire la comparaison par code 
        return (ExpenseType)expenseTypeDto;
    }
}
