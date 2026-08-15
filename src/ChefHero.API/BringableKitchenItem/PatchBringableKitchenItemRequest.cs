using System.Text.Json.Serialization;

using ChefHero.Application.BringableKitchenItem;

namespace ChefHero.API.BringableKitchenItem;

public class PatchBringableKitchenItemRequest
{
    private string? _name;
    private string? _description;

    [JsonPropertyName("name")]
    public string? Name
    {
        get => _name;
        set
        {
            _name = value;
            HasName = true;
        }
    }

    [JsonPropertyName("description")]
    public string? Description
    {
        get => _description;
        set
        {
            _description = value;
            HasDescription = true;
        }
    }

    [JsonIgnore]
    public bool HasName { get; private set; }

    [JsonIgnore]
    public bool HasDescription { get; private set; }

    public PatchBringableKitchenItemCommand ToCommand()
    {
        return new PatchBringableKitchenItemCommand(
            Name,
            Description,
            HasName,
            HasDescription);
    }
}