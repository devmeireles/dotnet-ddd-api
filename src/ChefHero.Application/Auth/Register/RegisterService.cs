using ChefHero.Application.Auth.Password;
using ChefHero.Application.Auth.Token;
using ChefHero.Application.Common.Exceptions;
using ChefHero.Application.Users;

using ChefHero.Domain.User;

namespace ChefHero.Application.Auth.Register;

public class RegisterService : IRegisterService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;

    public RegisterService(
        IPasswordHasher passwordHasher,
        ITokenService tokenService,
        IUserRepository userRepository)
    {
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
        _userRepository = userRepository;
    }

    public async Task<RegisterResult> RegisterAsync(
        RegisterCommand command,
        CancellationToken cancellationToken)
    {
        Email email = Email.Create(command.Email);

        User? existingUser = await _userRepository.GetByEmailAsync(
            email,
            cancellationToken);

        if (existingUser is not null)
        {
            throw new ConflictException(
                $"User with email '{email.Value}' already exists.");
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
            zipCode: command.ZipCode);

        await _userRepository.AddAsync(
            user,
            cancellationToken);

        await _userRepository.SaveChangesAsync(
            cancellationToken);

        string token = _tokenService.Generate(
            user.Id,
            user.Email.Value,
            user.Role.ToString());

        return new RegisterResult
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email.Value,
            Token = token
        };
    }
}