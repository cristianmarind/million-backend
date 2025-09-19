using Million.Domain.Exceptions;

namespace Million.Domain.Entities;

public class PropertyTrace
{
    public DateTime DateSale { get; private set; }
    public string Name { get; private set; }       // Nombre del comprador/vendedor
    public decimal Value { get; private set; }     // Valor de la transacción
    public decimal Tax { get; private set; }       // Impuesto aplicado

    public PropertyTrace(DateTime dateSale, string name, decimal value, decimal tax) {
        if (dateSale == default)
            throw new PropertyNotFoundException("DateSale must be valid");

        if (string.IsNullOrWhiteSpace(name))
            throw new PropertyNotFoundException("Name is required");

        if (value <= 0)
            throw new PropertyContentInvalidException("Value must be positive");

        if (tax < 0)
            throw new PropertyContentInvalidException("Tax cannot be negative");
        /*
            Additional validations can be added here
        */

        DateSale = dateSale;
        Name = name;
        Value = value;
        Tax = tax;
    }
}