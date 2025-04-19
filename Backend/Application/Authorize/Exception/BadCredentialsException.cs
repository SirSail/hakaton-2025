namespace Application.Authorization.Exceptions
{
    public class BadCredentialsException : Exception
    {
        public BadCredentialsException(string message) : base(message) { }
    }
}
