using ChefHero.Application.BringableKitchenItem;

namespace ChefHero.API.BringableKitchenItem;

public static class BringableKitchenItemMapper
{
    public static BringableKitchenItemCommand ToCommand(
        this BringableKitchenItemRequest request)
    {
        return new BringableKitchenItemCommand
        {
            Name = request.Name,
            Description = request.Description
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
}