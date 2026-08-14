using ChefHero.Application.Auth.Password;
using ChefHero.Application.Auth.Token;
using ChefHero.Application.Common.Exceptions;
using ChefHero.Application.Users;
using ChefHero.Domain.User;

namespace ChefHero.Application.Auth.Register;

public class RegisterService : IRegisterService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public RegisterService(IPasswordHasher passwordHasher, IUserRepository userRepository, ITokenService tokenService)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public RegisterResult Register(RegisterCommand command)
    {
        Email email = Email.Create(command.Email);

        User? existingUser = _userRepository.GetByEmail(email);

        if (existingUser is not null)
        {
            throw new ConflictException($"User with email '{email.Value}' already exists.");
        }

        string passwordHash = _passwordHasher.Hash(command.Password);

        User user = User.Create(
            name: command.Name,
            email: email,
            passwordHash: passwordHash,
            role: UserRole.User,
            phone: command.Phone,
            addressLine: command.AddressLine,
            city: command.City,
            state: command.State,
            zipCode: command.ZipCode
        );

        _userRepository.Add(user);
        _userRepository.SaveChanges();

        string token = _tokenService.Generate(
            user.Id,
            user.Email.Value,
            user.Role.ToString()
        );

        return new RegisterResult
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email.Value,
            Token = token
        };
    }
}