namespace MAM.WebApi.Middleware.Exceptions;

public class InternalException : Exception
{
    public InternalException(string message)
        : base(message)
    {
    }
}
