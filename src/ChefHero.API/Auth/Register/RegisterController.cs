using ChefHero.Application.Auth.Register;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ChefHero.API.Auth.Register;

[ApiController]
[Route("auth")]
public class RegisterController : ControllerBase
{
    private readonly IRegisterService _registerService;

    public RegisterController(IRegisterService registerService)
    {
        _registerService = registerService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(
    RegisterRequest request,
    CancellationToken cancellationToken)
    {
        RegisterCommand command = request.ToCommand();

        RegisterResult result = await _registerService.RegisterAsync(
            command,
            cancellationToken);

        RegisterResponse response = result.ToResponse();

        return Created(string.Empty, response);
    }
}