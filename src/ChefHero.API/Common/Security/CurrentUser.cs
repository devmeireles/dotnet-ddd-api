using System.Security.Claims;

using ChefHero.Application.Common.Security;

namespace ChefHero.API.Common.Security;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId
    {
        get
        {
            string? value = _httpContextAccessor.HttpContext?
                .User
                .FindFirst(ClaimTypes.NameIdentifier)?
                .Value;

            if (!Guid.TryParse(value, out Guid userId))
            {
                throw new UnauthorizedAccessException(
                    "Authenticated user ID is invalid.");
            }

            return userId;
        }
    }

    public string Email
    {
        get
        {
            string? email = _httpContextAccessor.HttpContext?
                .User
                .FindFirst(ClaimTypes.Email)?
                .Value;

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new UnauthorizedAccessException(
                    "Authenticated user email is missing.");
            }

            return email;
        }
    }

    public string Role
    {
        get
        {
            string? role = _httpContextAccessor.HttpContext?
                .User
                .FindFirst(ClaimTypes.Role)?
                .Value;

            if (string.IsNullOrWhiteSpace(role))
            {
                throw new UnauthorizedAccessException(
                    "Authenticated user role is missing.");
            }

            return role;
        }
    }
}