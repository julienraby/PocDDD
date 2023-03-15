using PocDDD.Api.Application.User;
using PocDDD.Api.Application.User.Ports.ServerSide;
using PocDDD.Api.Application.User.Ports.UserSide;
using PocDDD.Api.Infrastructure.User;

namespace PocDDD.Api.Injection;

public static class UserInjections
{
    public static void InjectUser(this WebApplicationBuilder webApplicationBuilder)
    {
        webApplicationBuilder.Services.AddScoped<IUserRepository, UserRepository>();
        webApplicationBuilder.Services.AddScoped<IPocDDDUserService, PocDDDUserService>(); 
    }
}