using PocDDD.Api.Domain.User;

namespace PocDDD.Api.Application.User.Ports.UserSide;

public interface IUserService
{
    Task<Domain.User.User> GetUser(Guid userId);
}