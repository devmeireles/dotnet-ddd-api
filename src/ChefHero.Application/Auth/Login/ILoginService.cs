namespace ChefHero.Application.Auth.Login;

public interface ILoginService
{
    LoginResult Login(LoginCommand command);
}