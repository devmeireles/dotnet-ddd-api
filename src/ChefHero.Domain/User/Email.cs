using System.Net.Mail;

namespace ChefHero.Domain.User;

public sealed class Email : IEquatable<Email>
{
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

        if (normalizedValue.Length > 320)
        {
            throw new ArgumentException(
                "Email cannot exceed 320 characters.",
                nameof(value));
        }

        if (!IsValid(normalizedValue))
        {
            throw new ArgumentException(
                "Email is invalid.",
                nameof(value));
        }

        return new Email(normalizedValue);
    }

    public bool Equals(Email? other)
    {
        if (other is null)
        {
            return false;
        }

        return string.Equals(
            Value,
            other.Value,
            StringComparison.OrdinalIgnoreCase);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Email);
    }

    public override int GetHashCode()
    {
        return StringComparer.OrdinalIgnoreCase.GetHashCode(Value);
    }

    private static bool IsValid(string value)
    {
        try
        {
            MailAddress mailAddress = new(value);

            return string.Equals(
                mailAddress.Address,
                value,
                StringComparison.OrdinalIgnoreCase);
        }
        catch (FormatException)
        {
            return false;
        }
    }
}