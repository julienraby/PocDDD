﻿using PocDDD.Api.Application.User.Ports.ServerSide;
using PocDDD.Api.Data;

namespace PocDDD.Api.Infrastructure.User;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _appDbContext;

    public UserRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Domain.User.PocDDDUser?> Get(Guid userId)
    {
        var userDbo = await _appDbContext.Users.FindAsync(userId);
        if (userDbo != null)
        {
            return PocDDDUserMapper.MapFromUserDto(userDbo);
        }
        else 
        { 
            return null; 
        }
    }
}