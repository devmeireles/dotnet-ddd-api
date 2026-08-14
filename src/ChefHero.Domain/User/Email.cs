namespace ChefHero.Domain.User;

public sealed class Email
{
    private const int MaxLength = 128;

    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(
                "Email cannot be empty.",
                nameof(value));
        }

        string normalizedValue = value.Trim();

        if (normalizedValue.Length > MaxLength)
        {
            throw new ArgumentException(
                $"Email cannot exceed {MaxLength} characters.",
                nameof(value));
        }

        if (!IsValidFormat(normalizedValue))
        {
            throw new ArgumentException(
                "Email has an invalid format.",
                nameof(value));
        }

        return new Email(normalizedValue);
    }

    private static bool IsValidFormat(string value)
    {
        int atIndex = value.IndexOf('@');

        if (atIndex <= 0)
        {
            return false;
        }

        if (atIndex != value.LastIndexOf('@'))
        {
            return false;
        }

        if (atIndex == value.Length - 1)
        {
            return false;
        }

        string domain = value[(atIndex + 1)..];

        return domain.Contains('.');
    }
}