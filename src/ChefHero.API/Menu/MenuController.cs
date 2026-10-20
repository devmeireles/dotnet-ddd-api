using ChefHero.API.Menu.Request;
using ChefHero.API.Menu.Response;
using ChefHero.Application.Common.Pagination;
using ChefHero.Application.Menu;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChefHero.API.Menu;

[Authorize]
[ApiController]
[Route("menus")]
public class MenuController : ControllerBase
{
    private readonly IMenuService _menuService;

    public MenuController(IMenuService menuService)
    {
        _menuService = menuService;
    }

    [HttpGet]
    [EndpointSummary("List menus with pagination and optional filters.")]
    public async Task<IActionResult> GetAllAsync(
        [FromQuery] GetMenusRequest request,
        CancellationToken cancellationToken = default
    )
    {
        PagedResult<MenuResult> result = await _menuService.GetAllAsync(
            request.Page,
            request.PageSize,
            request.SearchTerm,
            request.IsActive,
            cancellationToken);

        return Ok(result.ToResponse());
    }

    [HttpGet("{id:guid}", Name = "GetMenuById")]
    [EndpointSummary("Get a menu by its ID.")]
    public async Task<IActionResult> GetMenuByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        MenuResult? result = await _menuService.GetByIdAsync(
            id,
            cancellationToken);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result.ToResponse());
    }

    [HttpPost]
    [EndpointSummary("Create a menu.")]
    public async Task<IActionResult> CreateMenuAsync(
        MenuRequest request,
        CancellationToken cancellationToken = default)
    {
        MenuResult result = await _menuService.CreateAsync(
            request.ToCommand(),
            cancellationToken);

        MenuResponse response = result.ToResponse();

        return CreatedAtRoute("GetMenuById", new { id = result.Id }, response);
    }

    [HttpPatch("{id:guid}")]
    [EndpointSummary("Update the provided fields of a menu.")]
    public async Task<IActionResult> UpdateMenuAsync(
        Guid id,
        PatchMenuRequest request,
        CancellationToken cancellationToken = default)
    {
        MenuResult? result = await _menuService.PatchAsync(
            id,
            request.ToCommand(),
            cancellationToken);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result.ToResponse());
    }

    [HttpPut("{id:guid}/activate")]
    [EndpointSummary("Activate a menu.")]
    public async Task<IActionResult> ActivateAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        bool activated = await _menuService.ActivateAsync(
            id,
            cancellationToken);

        return activated ? NoContent() : NotFound();
    }

    [HttpPut("{id:guid}/deactivate")]
    [EndpointSummary("Deactivate a menu.")]
    public async Task<IActionResult> DeactivateAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        bool deactivated = await _menuService.DeactivateAsync(
            id,
            cancellationToken);

        return deactivated ? NoContent() : NotFound();
    }
}