using PocDDD.Api.Domain.User;

namespace PocDDD.Api.Application.User.Ports.UserSide;

public interface IPocDDDUserService
{
    Task<PocDDDUser> GetPocDDDUser(Guid userId);
}