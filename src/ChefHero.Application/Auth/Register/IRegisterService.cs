namespace ChefHero.Application.Auth.Register;

public interface IRegisterService
{
    RegisterResult Register(RegisterCommand command);
}