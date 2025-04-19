namespace Application.Authorization.Exceptions
{
    public static class BadCredentialsExceptionHelper
    {
        public static void ThrowEmailNotFound(string email)
        {
            throw new BadCredentialsException($"Nie znaleziono konta o emailu {email}");
        }

        public static void ThrowInvalidPassword()
        {
            throw new BadCredentialsException("Podano nieprawidłowe hasło");
        }

    }
}
