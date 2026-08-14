namespace ChefHero.Application.Auth.Register;

public interface IRegisterService
{
    Task<RegisterResult> RegisterAsync(
        RegisterCommand command,
        CancellationToken cancellationToken);
}