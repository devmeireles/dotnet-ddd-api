using ChefHero.API.BringableKitchenItem.Request;
using ChefHero.API.BringableKitchenItem.Response;
using ChefHero.Application.BringableKitchenItem;

namespace ChefHero.API.BringableKitchenItem.Mapper;

public static class BringableKitchenItemMapper
{
    public static BringableKitchenItemCommand ToCommand(
        this BringableKitchenItemRequest request)
    {
        return new BringableKitchenItemCommand
        {
            Name = request.Name,
            Description = request.Description ?? string.Empty
        };
    }

    public static BringableKitchenItemResponse ToResponse(
        this BringableKitchenItemResult result)
    {
        return new BringableKitchenItemResponse
        {
            Id = result.Id,
            Name = result.Name,
            Description = result.Description
        };
    }

    public static PagedBringableKitchenItemResponse ToResponse(
        this PagedBringableKitchenItemResult result)
    {
        return new PagedBringableKitchenItemResponse
        {
            Items = result.Items
                .Select(item => item.ToResponse())
                .ToList(),

            CurrentPage = result.CurrentPage,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount,
            TotalPages = result.TotalPages,
            HasPrevious = result.HasPrevious,
            HasNext = result.HasNext
        };
    }
}