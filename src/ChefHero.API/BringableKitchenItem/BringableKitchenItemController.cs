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
        CancellationToken cancellationToken)
    {
        IEnumerable<BringableKitchenItemResult> results =
            await _bringableKitchenItemService.GetAllAsync(cancellationToken);

        return Ok(
            results.Select(result => result.ToResponse()));
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
        BringableKitchenItemCommand command =
            request.ToCommand();

        BringableKitchenItemResult result =
            await _bringableKitchenItemService.CreateAsync(
                command,
                cancellationToken);

        return CreatedAtRoute(
            "GetBringableKitchenItemById",
            new { id = result.Id },
            result.ToResponse());
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(
        Guid id,
        BringableKitchenItemRequest request,
        CancellationToken cancellationToken)
    {
        BringableKitchenItemCommand command =
            request.ToCommand();

        BringableKitchenItemResult? result =
            await _bringableKitchenItemService.UpdateAsync(
                id,
                command,
                cancellationToken);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result.ToResponse());
    }
}