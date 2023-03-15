using PocDDD.Api.Application.Expense.Ports.ServerSide;
using PocDDD.Api.Application.User.Ports.ServerSide;
using PocDDD.Api.Common;
using PocDDD.Api.Domain.Expense;
using PocDDD.Api.Tests.API.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Immutable;

namespace PocDDD.Api.API.User.Tests;

public class ExpensesControllerTests
{
    private readonly ExpenseController _expenseController;
    private readonly Mock<IExpenseRepository> _mockExpenseRepository;
    private readonly Mock<IUserRepository> _mockUserRepository;

    public ExpensesControllerTests()
    {
        _mockExpenseRepository = new Mock<IExpenseRepository>();
        _mockUserRepository = new Mock<IUserRepository>();
        _expenseController = new ExpenseControllerBuilder()
            .WithApiConfigurations(
            Options.Create(new ApiConfiguration
            {
                MaximumNumberOfMonths = 3
            }))
            .WithExpenseRepository(_mockExpenseRepository)
            .WithUserRepository(_mockUserRepository)
            .Build();
    }

    [Fact]
    public async void BeSet()
    {
        var userId = new Guid();
        var user = new Domain.User.PocDDDUser(userId, "RABY", "Julien", "EUR");
        var expenses = new List<Expense> { new Expense(new Guid(), DateTime.Now.AddDays(-7), ExpenseType.Restaurant, 42, "EUR", "Repas de bienvenue") }.ToImmutableArray();
        var expenseToSave = new Expense(new Guid(), DateTime.Now.AddDays(-2), ExpenseType.Restaurant, 42, "EUR", "Repas");

        _mockExpenseRepository.Setup(_ => _.Get(userId)).ReturnsAsync(expenses);
        _mockUserRepository.Setup(_ => _.Get(userId)).ReturnsAsync(user);

        await _expenseController.SetExpenses(userId, expenseToSave);

        _mockExpenseRepository.Verify(_ => _.Save(userId, expenseToSave), Times.Once);
    }

    [Fact]
    public async Task BeGet()
    {
        var userId = new Guid();

        var user = new Domain.User.PocDDDUser(userId, "RABY", "Julien", "USD");
        var expenses = new List<Expense> { new Expense(new Guid(), DateTime.Now.AddDays(-2), ExpenseType.Restaurant, 33, "USD", "Repas x") }.ToImmutableArray();

        _mockExpenseRepository.Setup(_ => _.Get(userId)).ReturnsAsync(expenses);
        _mockUserRepository.Setup(_ => _.Get(userId)).ReturnsAsync(user);

        var response = await _expenseController.GetExpenses(userId);

        var okResult = response.Should().BeOfType<OkObjectResult>().Subject;
        var additivesResult = okResult.Value.Should().BeAssignableTo<ExpensesUser>().Subject;

        var expensesUser = new ExpensesUser($"{user.FirstName} {user.LastName}", expenses);
        additivesResult.Should().BeEquivalentTo(expensesUser);
    }
}