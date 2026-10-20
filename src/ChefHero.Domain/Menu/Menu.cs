using ChefHero.Domain.Common.Exceptions;

namespace ChefHero.Domain.Menu;

public class Menu
{
    private const int NameMaxLength = 64;
    private const int DescriptionMaxLength = 256;

    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public int MinPeople { get; private set; }
    public int MaxPeople { get; private set; }
    public decimal Price { get; private set; }
    public bool IsActive { get; private set; }

    private Menu()
    {
    }

    private Menu(
        Guid id,
        string name,
        string description,
        int minPeople,
        int maxPeople,
        decimal price)
    {
        Id = id;
        Name = name;
        Description = description;
        MinPeople = minPeople;
        MaxPeople = maxPeople;
        Price = price;
        IsActive = true;
    }

    public static Menu Create(
        string name,
        string description,
        int minPeople,
        int maxPeople,
        decimal price)
    {
        ValidateRequired(
            name,
            nameof(name),
            NameMaxLength);

        ValidateRequired(
            description,
            nameof(description),
            DescriptionMaxLength);

        ValidatePrice(price);
        ValidatePeopleRange(minPeople, maxPeople);

        return new Menu(
            Guid.NewGuid(),
            name.Trim(),
            description.Trim(),
            minPeople,
            maxPeople,
            price);
    }

    public void Update(
        string name,
        string description,
        int minPeople,
        int maxPeople,
        decimal price)
    {
        ValidateRequired(
            name,
            nameof(name),
            NameMaxLength);

        ValidateRequired(
            description,
            nameof(description),
            DescriptionMaxLength);

        ValidatePrice(price);
        ValidatePeopleRange(minPeople, maxPeople);

        Name = name.Trim();
        Description = description.Trim();
        MinPeople = minPeople;
        MaxPeople = maxPeople;
        Price = price;
    }

    private static void ValidateRequired(
        string value,
        string propertyName,
        int maxLength)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainValidationException(
                $"{propertyName} is required.");
        }

        if (value.Length > maxLength)
        {
            throw new DomainValidationException(
                $"{propertyName} cannot exceed {maxLength} characters.");
        }
    }

    private static void ValidatePrice(decimal price)
    {
        if (price <= 0)
        {
            throw new DomainValidationException(
                "Price must be greater than zero.");
        }
    }

    private static void ValidatePeopleRange(
        int minPeople,
        int maxPeople)
    {
        if (minPeople < 1)
        {
            throw new DomainValidationException(
                "Minimum number of people must be at least 1.");
        }

        if (maxPeople < minPeople)
        {
            throw new DomainValidationException(
                "Maximum number of people cannot be less than minimum number of people.");
        }
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
