namespace reservation_backend.Exceptions;

public class AuthException : Exception
{
    public AuthException(string message) : base(message){}
}