using ChefHero.Application.Auth.Me;

namespace ChefHero.API.Auth.Me.Mapper;

public static class MeMapper
{
    public static MeResponse ToResponse(this MeResult result)
    {
        return new MeResponse
        {
            Id = result.Id,
            Email = result.Email,
            Role = result.Role
        };
    }
}