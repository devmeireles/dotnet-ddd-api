namespace ChefHero.Domain.User
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public Email Email { get; private set; } = null!;
        public string PasswordHash { get; private set; } = string.Empty;
        public UserRole Role { get; private set; }
        public string Phone { get; private set; } = string.Empty;
        public string AddressLine { get; private set; } = string.Empty;
        public string City { get; private set; } = string.Empty;
        public string State { get; private set; } = string.Empty;
        public string ZipCode { get; private set; } = string.Empty;
        public bool IsActive { get; private set; }

        private User()
        {
        }

        private User(
            Guid id,
            string name,
            Email email,
            string passwordHash,
            UserRole role,
            string phone,
            string addressLine,
            string city,
            string state,
            string zipCode
        )
        {
            Id = id;
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
            Phone = phone;
            AddressLine = addressLine;
            City = city;
            State = state;
            ZipCode = zipCode;
            IsActive = true;
        }

        public static User Create(
            string name,
            Email email,
            string passwordHash,
            UserRole role,
            string phone,
            string addressLine,
            string city,
            string state,
            string zipCode
        )
        {
            ValidateRequired(name, nameof(name), 64);
            ValidateRequired(passwordHash, nameof(passwordHash), 256);
            ValidateRequired(phone, nameof(phone), 32);
            ValidateRequired(addressLine, nameof(addressLine), 128);
            ValidateRequired(city, nameof(city), 64);
            ValidateRequired(state, nameof(state), 64);
            ValidateRequired(zipCode, nameof(zipCode), 16);

            if (!Enum.IsDefined(role))
            {
                throw new ArgumentException(
                    "Invalid user role.",
                    nameof(role));
            }

            return new User(
                Guid.NewGuid(),
                name.Trim(),
                email,
                passwordHash,
                role,
                phone.Trim(),
                addressLine.Trim(),
                city.Trim(),
                state.Trim(),
                zipCode.Trim()
            );
        }

        public void Activate()
        {
            IsActive = true;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void ChangeRole(UserRole role)
        {
            Role = role;
        }

        public void ChangeName(string name)
        {
            ValidateRequired(name, nameof(name), 64);

            Name = name;
        }


        public void ChangePhone(string phone)
        {
            ValidateRequired(phone, nameof(phone), 32);

            Phone = phone;
        }

        public void ChangeAddress(
            string addressLine,
            string city,
            string state,
            string zipCode
        )
        {
            ValidateRequired(addressLine, nameof(addressLine), 128);
            ValidateRequired(city, nameof(city), 64);
            ValidateRequired(state, nameof(state), 64);
            ValidateRequired(zipCode, nameof(zipCode), 16);

            AddressLine = addressLine;
            City = city;
            State = state;
            ZipCode = zipCode;
        }

        private static void ValidateRequired(string value, string parameterName, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(
                    $"{parameterName} cannot be empty.",
                    parameterName);
            }

            if (value.Length > maxLength)
            {
                throw new ArgumentException(
                    $"{parameterName} cannot exceed {maxLength} characters.",
                    parameterName);
            }

        }
    }
}