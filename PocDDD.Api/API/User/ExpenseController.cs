using PocDDD.Api.Application.Expense.Ports.UserSide;
using PocDDD.Api.Application.User.Ports.UserSide;
using PocDDD.Api.Common;
using PocDDD.Api.Domain.Expense;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace PocDDD.Api.API.User;

[ApiController]
[Route("")]
public sealed class ExpenseController : ControllerBase
{
    private readonly ApiConfiguration _apiConfiguration;
    private readonly IExpenseUserService _expenseUserService;
    private readonly IUserService _PocDDDUserService;

    public ExpenseController(IOptions<ApiConfiguration> apiConfiguration, IExpenseUserService expenseUserService, IUserService PocDDDUserService)
    {
        _apiConfiguration = apiConfiguration.Value;
        _expenseUserService = expenseUserService;
        _PocDDDUserService = PocDDDUserService;
    }

    /// <summary>
    /// List of expenses
    /// </summary>
    /// <remarks>Retrieve all the information of expenses by user</remarks>
    /// <param name="userId">Internal user identifier</param>
    [HttpGet("/user/{userId}/expenses")]
    [SwaggerOperation(Tags = new[] { "User" })]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(ExpensesUser), description: "successful operation")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, description: "Unauthorized")]
    public async Task<IActionResult> GetExpenses([FromRoute][Required] Guid userId)
    {
        var expenses = await _expenseUserService.GetExpenses(userId);
        if (expenses != null)
        {
            return Ok(expenses);
        }
        else
        {
            return StatusCode(StatusCodes.Status401Unauthorized);
        }
    }

    /// <summary>
    /// Create expense
    /// </summary>
    /// <param name="userId">Internal user identifier</param>
    /// <param name="expense">Information of expense</param>
    [HttpPut("/user/{userId}/expenses")]
    [SwaggerOperation(Tags = new[] { "User" })]
    [SwaggerResponse(StatusCodes.Status204NoContent, "successful operation")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, description: "Unauthorized")]
    public async Task<IActionResult> SetExpenses([FromRoute][Required] Guid userId, Expense expense)
    {
        var user = await _PocDDDUserService.GetUser(userId);
        if (user == null)
        {
            return StatusCode(StatusCodes.Status401Unauthorized);
        }

        if (expense.From > DateTime.Now)
        {
            return StatusCode(StatusCodes.Status412PreconditionFailed, "Une dépense ne peut pas avoir une date dans le futur");
        }

        if (expense.From.Date < DateTime.Now.Date.AddMonths(-_apiConfiguration.MaximumNumberOfMonths))
        {
            return StatusCode(StatusCodes.Status412PreconditionFailed, $"Une dépense ne peut pas être datée de plus de {_apiConfiguration.MaximumNumberOfMonths} mois");
        }

        if (string.IsNullOrWhiteSpace(expense.Commentary))
        {
            return StatusCode(StatusCodes.Status412PreconditionFailed, "Le commentaire est obligatoire");
        }

        if (await CheckDuplicateExpense(user.Id, expense))
        {
            return StatusCode(StatusCodes.Status412PreconditionFailed, "Un utilisateur ne peut pas déclarer deux fois la même dépense(même date et même montant)");
        }

        if (expense.Currency != user.Currency)
        {
            return StatusCode(StatusCodes.Status412PreconditionFailed, "La devise de la dépense doit être identique à celle de l'utilisateur");
        }

        await _expenseUserService.Save(userId, expense);

        return NoContent();
    }

    private async Task<bool> CheckDuplicateExpense(Guid userId, Expense expense)
    {
        var isDuplicate = false;
        var expenses = await _expenseUserService.GetExpenses(userId);

        if (expenses.Expenses.Any())
        {
            isDuplicate = expenses.Expenses.Any(e => e.Mounth == expense.Mounth && e.From.Date == expense.From.Date);
        }

        return isDuplicate;
    }
}