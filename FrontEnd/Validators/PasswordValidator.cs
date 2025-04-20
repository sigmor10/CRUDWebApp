using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Validators
{
    // Custom password for use in password validation
    public class PasswordValidator : ValidationAttribute
    {
        public bool IsEdit { get; set; }

        public override bool IsValid(object value)
        {
            var password = value as string;

            if (string.IsNullOrEmpty(password) && IsEdit)
                return true;
            

            return !string.IsNullOrEmpty(password) &&
                   password.Length >= 8 &&
                   password.Any(char.IsUpper) &&
                   password.Any(char.IsLower) &&
                   password.Any(char.IsDigit) &&
                   password.Any(c => !char.IsLetterOrDigit(c));
        }

        public override string FormatErrorMessage(string name)
        {
            return "Hasło musi zawierać co najmniej 8 znaków, w tym jedną wielką literę, jedną małą literę, jedną cyfrę i jeden znak specjalny.";
        }
    }
}
