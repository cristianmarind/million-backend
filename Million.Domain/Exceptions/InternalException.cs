namespace Million.Domain.Exceptions;

public abstract class InternalException : Exception
{
    protected InternalException(string message) : base(message) { }
}