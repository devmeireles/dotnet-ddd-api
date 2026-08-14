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
    public IActionResult Register(RegisterRequest request)
    {
        RegisterCommand command = request.ToCommand();
        RegisterResult result = _registerService.Register(command);
        RegisterResponse response = result.ToResponse();

        return StatusCode(StatusCodes.Status201Created, response);
    }
}