using PocDDD.Api.Domain.User;

namespace PocDDD.Api.Infrastructure.User;

public sealed class UserMapper
{
    public static Domain.User.User MapFromUserDto(UserDto userDto)
    {
        return new Domain.User.User(userDto.Id, 
            userDto.LastName, 
            userDto.FirstName, 
            userDto.Currency);
    }
}
