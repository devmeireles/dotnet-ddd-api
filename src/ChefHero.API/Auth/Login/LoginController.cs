using ChefHero.API.Auth.Login.Mapper;
using ChefHero.API.Auth.Login.Request;
using ChefHero.API.Auth.Login.Response;
using ChefHero.Application.Auth.Login;

using Microsoft.AspNetCore.Mvc;

namespace ChefHero.API.Auth.Login;

[ApiController]
[Route("auth")]
public class LoginController : ControllerBase
{
    private readonly ILoginService _loginService;

    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(
        LoginRequest request,
        CancellationToken cancellationToken)
    {
        LoginCommand command = request.ToCommand();
        LoginResult result = await _loginService.LoginAsync(command, cancellationToken);
        LoginResponse response = result.ToResponse();

        return Ok(response);
    }
}