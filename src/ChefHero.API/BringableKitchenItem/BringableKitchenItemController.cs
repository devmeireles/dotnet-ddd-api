using ChefHero.Application.BringableKitchenItem;

using Microsoft.AspNetCore.Mvc;

namespace ChefHero.API.BringableKitchenItem;

[ApiController]
[Route("bringable-kitchen-items")]
public class BringableKitchenItemController : ControllerBase
{
    private readonly IBringableKitchenItemService _bringableKitchenItemService;

    public BringableKitchenItemController(
        IBringableKitchenItemService bringableKitchenItemService)
    {
        _bringableKitchenItemService = bringableKitchenItemService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(
        [FromQuery] GetBringableKitchenItemsRequest request,
        CancellationToken cancellationToken = default)
    {
        PagedBringableKitchenItemResult result =
            await _bringableKitchenItemService.GetAllAsync(
                request.Page,
                request.PageSize,
                request.SearchTerm,
                request.IsActive,
                cancellationToken);

        return Ok(result.ToResponse());
    }

    [HttpGet("{id:guid}", Name = "GetBringableKitchenItemById")]
    public async Task<IActionResult> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        BringableKitchenItemResult? result =
            await _bringableKitchenItemService.GetByIdAsync(
                id,
                cancellationToken);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result.ToResponse());
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(
        BringableKitchenItemRequest request,
        CancellationToken cancellationToken)
    {
        BringableKitchenItemResult result =
            await _bringableKitchenItemService.CreateAsync(
                request.ToCommand(),
                cancellationToken);

        BringableKitchenItemResponse response =
            result.ToResponse();

        return CreatedAtRoute(
            "GetBringableKitchenItemById",
            new { id = response.Id },
            response);
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> PatchAsync(
        Guid id,
        PatchBringableKitchenItemRequest request,
        CancellationToken cancellationToken)
    {
        PatchBringableKitchenItemCommand command =
            request.ToCommand();

        BringableKitchenItemResult? result =
            await _bringableKitchenItemService.PatchAsync(
                id,
                command,
                cancellationToken);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result.ToResponse());
    }

    [HttpPost("{id:guid}/activate")]
    public async Task<IActionResult> ActivateAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        bool activated =
            await _bringableKitchenItemService.ActivateAsync(
                id,
                cancellationToken);

        if (!activated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost("{id:guid}/deactivate")]
    public async Task<IActionResult> DeactivateAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        bool deactivated =
            await _bringableKitchenItemService.DeactivateAsync(
                id,
                cancellationToken);

        if (!deactivated)
        {
            return NotFound();
        }

        return NoContent();
    }
}