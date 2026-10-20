using BringableKitchenItemEntity =
ChefHero.Domain.BringableKitchenItem.BringableKitchenItem;

namespace ChefHero.Application.BringableKitchenItem.Mapper;

public static class BringableKitchenItemMapper
{
    public static BringableKitchenItemResult ToResult(
    this BringableKitchenItemEntity item)
    {
        return new BringableKitchenItemResult
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description
        };
    }
}
