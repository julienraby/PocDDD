using PocDDD.Api.Application.User.Ports.ServerSide;
using PocDDD.Api.Application.User.Ports.UserSide;
using PocDDD.Api.Domain.User;

namespace PocDDD.Api.Application.User;

public class PocDDDUserService : IPocDDDUserService
{
    private readonly IUserRepository _userRepository;

    public PocDDDUserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<PocDDDUser> GetPocDDDUser(Guid userId)
    {
        return await _userRepository.Get(userId);
    }
}