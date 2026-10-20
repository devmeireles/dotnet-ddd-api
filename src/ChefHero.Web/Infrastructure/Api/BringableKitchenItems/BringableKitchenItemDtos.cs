namespace ChefHero.Web.Infrastructure.Api.BringableKitchenItems;

public sealed record CreateBringableKitchenItemRequest(
    string Name,
    string? Description
);

public sealed record PatchBringableKitchenItemRequest(
    string Name,
    string? Description
);

public sealed record BringableKitchenItemResponse(
    Guid Id,
    string Name,
    string? Description
);

public sealed record PagedBringableKitchenItemsResponse(
    IReadOnlyCollection<BringableKitchenItemResponse> Items,
    int CurrentPage,
    int PageSize,
    int TotalCount,
    int TotalPages,
    bool HasPrevious,
    bool HasNext
);