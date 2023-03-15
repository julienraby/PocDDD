using PocDDD.Api.Domain.Expense;
using PocDDD.Api.Infrastructure.User;
using System.ComponentModel.DataAnnotations;

namespace PocDDD.Api.Infrastructure.Expense;

public class ExpenseDto
{
    [Key]
    public Guid Id { get; set; }
    public DateTime From { get; set; }
    public ExpenseTypeDto Type { get; set; }
    public decimal Mounth { get; set; }
    public string Currency { get; set; }
    public string Commentary { get; set; }

    public Guid? UserId { get; set; }
    public UserDto? User { get; set; }
}
