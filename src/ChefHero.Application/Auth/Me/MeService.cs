using ChefHero.Application.Common.Security;

namespace ChefHero.Application.Auth.Me;

public class MeService : IMeService
{
    private readonly ICurrentUser _currentUser;

    public MeService(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }

    public MeResult GetCurrentUser()
    {
        return new MeResult
        {
            Id = _currentUser.UserId,
            Email = _currentUser.Email,
            Role = _currentUser.Role
        };
    }
}