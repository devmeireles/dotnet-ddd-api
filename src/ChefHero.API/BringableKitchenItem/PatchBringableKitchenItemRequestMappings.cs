using ChefHero.Application.BringableKitchenItem;

namespace ChefHero.API.BringableKitchenItem;

public static class PatchBringableKitchenItemRequestMappings
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