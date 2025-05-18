using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TSUApplicationApi.Attributes
{
    public class PasswordComplexityAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is not string password) return false;

            if (password.Length < 8)
                return false;

            if (!Regex.IsMatch(password, @"[A-Z]")) 
                return false;

            if (!Regex.IsMatch(password, @"[a-z]")) 
                return false;

            if (!Regex.IsMatch(password, @"[0-9]")) 
                return false;

            if (!Regex.IsMatch(password, @"[^a-zA-Z0-9]")) 
                return false;

            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return "Password must be at least 8 characters long, and contain uppercase, lowercase, digit, and special character.";
        }
    }
}
