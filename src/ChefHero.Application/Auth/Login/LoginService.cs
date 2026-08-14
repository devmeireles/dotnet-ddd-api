using ChefHero.Application.Auth.Password;
using ChefHero.Application.Auth.Token;
using ChefHero.Application.Users;
using ChefHero.Domain.User;

namespace ChefHero.Application.Auth.Login;

public class LoginService : ILoginService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public LoginService(
        IPasswordHasher passwordHasher,
        IUserRepository userRepository,
        ITokenService tokenService)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<LoginResult> LoginAsync(
        LoginCommand command,
        CancellationToken cancellationToken)
    {
        Email email = Email.Create(command.Email);

        User? user = await _userRepository.GetByEmailAsync(email, cancellationToken);

        if (user is null ||
            !_passwordHasher.Verify(command.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException(
                "Invalid email or password.");
        }

        if (!user.IsActive)
        {
            throw new UnauthorizedAccessException(
                "Invalid email or password.");
        }

        string token = _tokenService.Generate(
            user.Id,
            user.Email.Value,
            user.Role.ToString());

        return new LoginResult
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email.Value,
            Token = token
        };
    }
}