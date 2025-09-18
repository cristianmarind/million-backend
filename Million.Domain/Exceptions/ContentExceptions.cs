namespace Million.Domain.Exceptions;

public class PropertyContentInvalidException : Exception {
    public PropertyContentInvalidException(string message) : base(message) { }
}