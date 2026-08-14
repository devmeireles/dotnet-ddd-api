using ChefHero.Application.Auth.Register;

namespace ChefHero.API.Auth.Register;

public static class RegisterMapper
{
    public static RegisterCommand ToCommand(this RegisterRequest request)
    {
        return new RegisterCommand
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
            Phone = request.Phone,
            AddressLine = request.AddressLine,
            City = request.City,
            State = request.State,
            ZipCode = request.ZipCode
        };
    }

    public static RegisterResponse ToResponse(this RegisterResult result)
    {
        return new RegisterResponse
        {
            Id = result.Id,
            Name = result.Name,
            Email = result.Email,
            Token = result.Token
        };
    }
}