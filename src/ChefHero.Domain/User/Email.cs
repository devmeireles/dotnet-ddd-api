using ChefHero.Domain.Common.Exceptions;

using System.Net.Mail;

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
            throw new DomainValidationException(
                "Email cannot be empty.");
        }

        string normalizedValue = value.Trim().ToLowerInvariant();

        if (normalizedValue.Length > MaxLength)
        {
            throw new DomainValidationException(
                $"Email cannot exceed {MaxLength} characters.");
        }

        if (!IsValidFormat(normalizedValue))
        {
            throw new DomainValidationException(
                "Email has an invalid format.");
        }

        return new Email(normalizedValue);
    }

    private static bool IsValidFormat(string value)
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