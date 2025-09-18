namespace Million.Domain.Entities;

public class PropertyTrace
{
    public DateTime DateSale { get; private set; }
    public string Name { get; private set; }       // Nombre del comprador/vendedor
    public decimal Value { get; private set; }     // Valor de la transacción
    public decimal Tax { get; private set; }       // Impuesto aplicado

    public PropertyTrace(DateTime dateSale, string name, decimal value, decimal tax) {
        if (dateSale == default)
            throw new ArgumentException("DateSale must be valid", nameof(dateSale));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required", nameof(name));

        if (value <= 0)
            throw new ArgumentException("Value must be positive", nameof(value));

        if (tax < 0)
            throw new ArgumentException("Tax cannot be negative", nameof(tax));
        /*
            Additional validations can be added here
        */

        DateSale = dateSale;
        Name = name;
        Value = value;
        Tax = tax;
    }
}