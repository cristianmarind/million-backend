namespace Million.Domain.Exceptions;

public class PropertyContentInvalidException : InternalException {
    public PropertyContentInvalidException(string message) : base(message) { }
}