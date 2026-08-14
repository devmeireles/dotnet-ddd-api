namespace ChefHero.Application.Auth.Login;

public interface ILoginService
{
    Task<LoginResult> LoginAsync(
        LoginCommand command,
        CancellationToken cancellationToken);
}