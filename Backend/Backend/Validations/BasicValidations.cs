using API.Exceptions;

namespace API.Validations
{
    public class BasicValidations
    {
        public static void ValidateEmail(string fieldName,string email)
        {
            if (!string.IsNullOrEmpty(email) && !System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new RequestValidationException(fieldName, "Podano niepoprawny email.");
            }
        }

        public static void ValidateTextNotEmpty(string fieldName, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new RequestValidationException(fieldName, "Pole nie może być puste");
            }
        }

        public static void ValidateTextLength(string fieldName,string value, int minLength = 0, int maxLength = 255)
        {
            if(!string.IsNullOrEmpty(value))
            {
                if (value.Length > maxLength)
                {
                    throw new RequestValidationException(fieldName, $"Pole za długie (max {maxLength} znaków");
                }
                if(value.Length < minLength)
                {
                    throw new RequestValidationException(fieldName, $"Pole za krótkie (min {minLength} znaków");
                }
            }
        }

        public static void ValidateTextExactLength(string fieldName,string value, int exactLength)
        {
            if(!string.IsNullOrEmpty(value))
            {
                if (value.Length != exactLength)
                {
                    throw new RequestValidationException(fieldName, $"Pole musi mieć dokładnie {exactLength} znaków");
                }
            }
        }

    }
}
