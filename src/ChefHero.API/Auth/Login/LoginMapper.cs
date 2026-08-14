using ChefHero.Application.Auth.Login;

namespace ChefHero.API.Auth.Login;

public static class LoginMapper
{
    public static LoginCommand ToCommand(this LoginRequest request)
    {
        return new LoginCommand
        {
            Email = request.Email,
            Password = request.Password
        };
    }

    public static LoginResponse ToResponse(this LoginResult result)
    {
        return new LoginResponse
        {
            Id = result.Id,
            Name = result.Name,
            Email = result.Email,
            Token = result.Token
        };
    }
}