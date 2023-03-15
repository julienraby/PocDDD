using PocDDD.Api.Application.User.Ports.ServerSide;
using PocDDD.Api.Application.User.Ports.UserSide;
using PocDDD.Api.Domain.User;

namespace PocDDD.Api.Application.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Domain.User.User> GetUser(Guid userId)
    {
        return await _userRepository.Get(userId);
    }
}