namespace PocDDD.Api.Domain.User;

public sealed record PocDDDUser(Guid Id, string LastName, string FirstName, string Currency)
{
    public override string ToString()
    {
        return $"{FirstName} {LastName.ToUpper()}";
    }
}
