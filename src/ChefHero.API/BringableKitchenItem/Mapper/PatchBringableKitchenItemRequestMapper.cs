using ChefHero.API.BringableKitchenItem.Request;
using ChefHero.Application.BringableKitchenItem;

namespace ChefHero.API.BringableKitchenItem.Mapper;

public static class PatchBringableKitchenItemRequestMapper
{
    public static PatchBringableKitchenItemCommand ToCommand(
        this PatchBringableKitchenItemRequest request)
    {
        return new PatchBringableKitchenItemCommand(
            request.Name,
            request.Description,
            request.HasName,
            request.HasDescription);
    }
}