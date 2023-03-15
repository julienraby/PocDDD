using PocDDD.Api.Infrastructure.Expense;
using PocDDD.Api.Infrastructure.User;
using Microsoft.EntityFrameworkCore;

namespace PocDDD.Api.Data;

public class DbInitializer
{
    private readonly ModelBuilder _builder;

    public DbInitializer(ModelBuilder builder)
    {
        _builder = builder;
    }

    public void Seed()
    {
        _builder.Entity<UserDto>(_ =>
        {
            _.HasData(new UserDto
            {
                Id = new Guid("CCC743D0-D71A-4D9C-B032-65803976FEC3"),
                FirstName = "Anthony",
                LastName = "Stark",
                Currency = "USD"

            });
            _.HasData(new UserDto
            {
                Id = new Guid("E899C5B1-F9B9-4F89-AD1B-F1A70D53FE0B"),
                FirstName = "Natasha",
                LastName = "Romanova",
                Currency = "RUB"
            });
        });

        _builder.Entity<ExpenseDto>();
    }
}
