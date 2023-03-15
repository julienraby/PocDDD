using PocDDD.Api.Domain.User;

namespace PocDDD.Api.Infrastructure.User;

public sealed class PocDDDUserMapper
{
    public static PocDDDUser MapFromUserDto(UserDto userDto)
    {
        return new PocDDDUser(userDto.Id, 
            userDto.LastName, 
            userDto.FirstName, 
            userDto.Currency);
    }
}
