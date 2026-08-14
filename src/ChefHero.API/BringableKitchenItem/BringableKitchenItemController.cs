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
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 20,
    CancellationToken cancellationToken = default)
    {
        PagedBringableKitchenItemResult result =
            await _bringableKitchenItemService.GetAllAsync(
                page,
                pageSize,
                cancellationToken);

        return Ok(result.ToResponse());
    }

    [HttpGet("{id:guid}")]
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

        return CreatedAtAction(
            nameof(GetByIdAsync),
            new { id = response.Id },
            response);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(
        Guid id,
        BringableKitchenItemRequest request,
        CancellationToken cancellationToken)
    {
        BringableKitchenItemResult? result =
            await _bringableKitchenItemService.UpdateAsync(
                id,
                request.ToCommand(),
                cancellationToken);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result.ToResponse());
    }
}