using PocDDD.Api.Domain.User;

namespace PocDDD.Api.Application.User.Ports.ServerSide;

public interface IUserRepository
{
    Task<PocDDDUser> Get(Guid userId);
}