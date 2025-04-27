namespace LibraryBookingSystem.Services
{
    public class SimplePasswordValidator
    {

        public List<string> Validate(string password)
        {
            var errors = new List<string>();

            if (!password.Any(char.IsUpper))
            {
                errors.Add("Password must contain at least one uppercase letter.");
            }

            if (!password.Any(char.IsLower))
            {
                errors.Add("Password must contain at least one lowercase letter.");
            }

            if (!password.Any(char.IsDigit))
            {
                errors.Add("Password must contain at least one number");
            }

            if(!password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                errors.Add("Password must contain at least one special character (e.g., !, @, #).");
            }

            return errors;
        }
    }
}
