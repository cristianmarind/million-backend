namespace Million.Domain.Exceptions;

public class PropertyNotFoundException : InternalException {
    public PropertyNotFoundException(string message) : base(message) { }
}