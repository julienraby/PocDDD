namespace PocDDD.Api.Domain.User;

public sealed record User(Guid Id, string LastName, string FirstName, string Currency)
{
    public override string ToString()
    {
        return $"{FirstName} {LastName.ToUpper()}";
    }
}
