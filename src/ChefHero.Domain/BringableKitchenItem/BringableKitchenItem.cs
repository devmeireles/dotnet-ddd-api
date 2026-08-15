using ChefHero.Domain.Common.Exceptions;

namespace ChefHero.Domain.BringableKitchenItem;

public sealed class BringableKitchenItem
{
    private const int NameMaxLength = 64;
    private const int DescriptionMaxLength = 256;

    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public bool IsActive { get; private set; }

    private BringableKitchenItem(
        Guid id,
        string name,
        string? description)
    {
        Id = id;
        Name = name;
        Description = description;
        IsActive = true;
    }

    public static BringableKitchenItem Create(
        string name,
        string? description)
    {
        ValidateRequired(
            name,
            nameof(name),
            NameMaxLength);

        ValidateOptional(
            description,
            nameof(description),
            DescriptionMaxLength);

        return new BringableKitchenItem(
            Guid.NewGuid(),
            name.Trim(),
            description?.Trim());
    }

    public void Update(
        string name,
        string? description)
    {
        ValidateRequired(
            name,
            nameof(name),
            NameMaxLength);

        ValidateOptional(
            description,
            nameof(description),
            DescriptionMaxLength);

        Name = name.Trim();
        Description = description?.Trim();
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    private static void ValidateRequired(
        string value,
        string propertyName,
        int maxLength)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainValidationException(
                $"The {propertyName} is required.");
        }

        if (value.Length > maxLength)
        {
            throw new DomainValidationException(
                $"The {propertyName} must not exceed {maxLength} characters.");
        }
    }

    private static void ValidateOptional(
        string? value,
        string propertyName,
        int maxLength)
    {
        if (value is not null && value.Length > maxLength)
        {
            throw new DomainValidationException(
                $"The {propertyName} must not exceed {maxLength} characters.");
        }
    }
}