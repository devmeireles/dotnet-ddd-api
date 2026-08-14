using ChefHero.Application.Auth.Me;
using ChefHero.Application.Common.Security;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChefHero.API.Auth.Me;

[ApiController]
[Route("auth")]
public class MeController : ControllerBase
{
    private readonly IMeService _meService;

    public MeController(IMeService meService)
    {
        _meService = meService;
    }

    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        MeResult result = _meService.GetCurrentUser();
        MeResponse response = result.ToResponse();

        return Ok(response);
    }
}