using PocDDD.Api.Infrastructure.Expense;
using System.ComponentModel.DataAnnotations;

namespace PocDDD.Api.Infrastructure.User
{
    public class UserDto
    {
        [Key]
        public Guid Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Currency { get; set; }

        public List<ExpenseDto> Expenses { get; set; }
    }
}
