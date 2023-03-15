using PocDDD.Api.Domain.User;

namespace PocDDD.Api.Application.User.Ports.ServerSide;

public interface IUserRepository
{
    Task<Domain.User.User> Get(Guid userId);
}