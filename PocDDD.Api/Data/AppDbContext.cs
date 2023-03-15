using PocDDD.Api.Infrastructure.Expense;
using PocDDD.Api.Infrastructure.User;
using Microsoft.EntityFrameworkCore;

namespace PocDDD.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<ExpenseDto> Expenses { get; set; }
    public DbSet<UserDto> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ExpenseDto>()
            .HasOne(x => x.User)
            .WithMany(x => x.Expenses);

        new DbInitializer(builder).Seed();
    }
}
