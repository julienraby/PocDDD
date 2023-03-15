using PocDDD.Api.API.User;
using PocDDD.Api.Application.Expense;
using PocDDD.Api.Application.Expense.Ports.ServerSide;
using PocDDD.Api.Application.User;
using PocDDD.Api.Application.User.Ports.ServerSide;
using PocDDD.Api.Common;
using Microsoft.Extensions.Options;

namespace PocDDD.Api.Tests.API.User;

internal sealed class ExpenseControllerBuilder
{
    private ExpenseUserService _expenseUserService;
    private PocDDDUserService _PocDDDUserService;
    private IOptions<ApiConfiguration> _apiConfigurations;
    private Mock<IExpenseRepository> _mockExpenseRepository;
    private Mock<IUserRepository> _mockUserRepository;

    public ExpenseControllerBuilder WithApiConfigurations(IOptions<ApiConfiguration> apiConfigurations)
    {
        _apiConfigurations = apiConfigurations;
        return this;
    }

    public ExpenseControllerBuilder WithExpenseRepository(Mock<IExpenseRepository> expenseRepository)
    {
        _mockExpenseRepository = expenseRepository;
        return this;
    }

    public ExpenseControllerBuilder WithUserRepository(Mock<IUserRepository> mockUserRepository)
    {
        _mockUserRepository = mockUserRepository;
        return this;
    }

    public ExpenseController Build()
    {
        _expenseUserService = new ExpenseUserService(_mockExpenseRepository.Object, _mockUserRepository.Object);
        _PocDDDUserService = new PocDDDUserService(_mockUserRepository.Object);
        return new ExpenseController(_apiConfigurations, _expenseUserService, _PocDDDUserService);
    }
}