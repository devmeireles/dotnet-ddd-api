using System.Text.Json.Serialization;

using ChefHero.Application.Menu;

namespace ChefHero.API.Menu.Request;

public class PatchMenuRequest
{
    private string? _name;
    private string? _description;
    private int? _minPeople;
    private int? _maxPeople;
    private decimal? _price;

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

    [JsonPropertyName("minPeople")]
    public int? MinPeople
    {
        get => _minPeople;
        set
        {
            _minPeople = value;
            HasMinPeople = true;
        }
    }

    [JsonPropertyName("maxPeople")]
    public int? MaxPeople
    {
        get => _maxPeople;
        set
        {
            _maxPeople = value;
            HasMaxPeople = true;
        }
    }

    [JsonPropertyName("price")]
    public decimal? Price
    {
        get => _price;
        set
        {
            _price = value;
            HasPrice = true;
        }
    }

    [JsonIgnore]
    public bool HasName { get; private set; }

    [JsonIgnore]
    public bool HasDescription { get; private set; }

    [JsonIgnore]
    public bool HasMinPeople { get; private set; }

    [JsonIgnore]
    public bool HasMaxPeople { get; private set; }

    [JsonIgnore]
    public bool HasPrice { get; private set; }

    public PatchMenuCommand ToCommand()
    {
        return new PatchMenuCommand(
            Name,
            Description,
            MinPeople,
            MaxPeople,
            Price,
            HasName,
            HasDescription,
            HasMinPeople,
            HasMaxPeople,
            HasPrice);
    }
}
