namespace Million.Domain.Exceptions;

public class PropertyNotFoundException : Exception {
    public PropertyNotFoundException(string message) : base(message) { }
}