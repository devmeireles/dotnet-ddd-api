using ChefHero.Domain.Common.Exceptions;

namespace ChefHero.Domain.MenuCourse;

public class MenuCourse
{
    private const int NameMaxLength = 64;
    private const int DescriptionMaxLength = 256;

    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public MenuCourseType Type { get; private set; }
    public Guid MenuId { get; private set; }

    private MenuCourse()
    {
    }

    private MenuCourse(
        Guid id,
        string name,
        string description,
        MenuCourseType type,
        Guid menuId)
    {
        Id = id;
        Name = name;
        Description = description;
        Type = type;
        MenuId = menuId;
    }

    public static MenuCourse Create(
        string name,
        string description,
        MenuCourseType type,
        Guid menuId)
    {
        ValidateRequired(
            name,
            nameof(name),
            NameMaxLength);

        ValidateRequired(
            description,
            nameof(description),
            DescriptionMaxLength);

        ValidateType(type);

        if (menuId == Guid.Empty)
        {
            throw new DomainValidationException(
                "MenuId is required.");
        }

        return new MenuCourse(
            Guid.NewGuid(),
            name.Trim(),
            description.Trim(),
            type,
            menuId);
    }

    public void Update(
        string name,
        string description)
    {
        ValidateRequired(
            name,
            nameof(name),
            NameMaxLength);

        ValidateRequired(
            description,
            nameof(description),
            DescriptionMaxLength);

        Name = name.Trim();
        Description = description.Trim();
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

    private static void ValidateType(MenuCourseType type)
    {
        if (!Enum.IsDefined(type))
        {
            throw new DomainValidationException(
                "Invalid menu course type.");
        }
    }

}
