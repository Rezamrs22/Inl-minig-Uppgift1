using System;
using System.Text.RegularExpressions;

namespace PrimeService;

public class UserRegistrationService
{
    private readonly List<User> _users = new List<User>();

    public RegistrationResult Register(User user)
    {
        if (!IsValidUsername(user.Username))
            return new RegistrationResult { IsSuccess = false, Message = "Username must be between 5 and 20 alphanumeric characters" };

        if (!IsValidPassword(user.Password))
            return new RegistrationResult { IsSuccess = false, Message = "Password must be at least 8 characters and include a special character" };

        if (!IsValidEmail(user.Email))
            return new RegistrationResult { IsSuccess = false, Message = "Invalid email format" };

        if (_users.Any(u => u.Username == user.Username))
            return new RegistrationResult { IsSuccess = false, Message = "Username already taken" };

        _users.Add(user);

        return new RegistrationResult { IsSuccess = true, Username = user.Username, Message = "Registration successful" };
    }

    private bool IsValidUsername(string username) =>
        !string.IsNullOrWhiteSpace(username) && username.Length >= 5 && username.Length <= 20 && Regex.IsMatch(username, @"^[a-zA-Z0-9]+$");

    private bool IsValidPassword(string password) =>
        !string.IsNullOrWhiteSpace(password) && password.Length >= 8 && Regex.IsMatch(password, @".*[\W_].*");

    private bool IsValidEmail(string email) =>
        Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
}
