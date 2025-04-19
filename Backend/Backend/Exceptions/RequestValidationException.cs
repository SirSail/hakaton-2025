namespace API.Exceptions
{
    public class RequestValidationException : Exception
    {
        public RequestValidationException(string fieldName,string message) : base($"Pole {fieldName}: {message}") { }
    }
}
