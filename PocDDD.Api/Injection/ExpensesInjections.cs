using PocDDD.Api.Application.Expense;
using PocDDD.Api.Application.Expense.Ports.ServerSide;
using PocDDD.Api.Application.Expense.Ports.UserSide;
using PocDDD.Api.Infrastructure.Expense;

namespace PocDDD.Api.Injection;

public static class ExpensesInjections
{
    public static void InjectExpenses(this WebApplicationBuilder webApplicationBuilder)
    {
        webApplicationBuilder.Services.AddScoped<IExpenseUserService, ExpenseUserService>();
        webApplicationBuilder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
    }
}
