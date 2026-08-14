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
    public IActionResult Login(LoginRequest request)
    {
        LoginCommand command = request.ToCommand();
        LoginResult result = _loginService.Login(command);
        LoginResponse response = result.ToResponse();

        return Ok(response);
    }
}