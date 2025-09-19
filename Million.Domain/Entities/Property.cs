using Million.Domain.ValueObjects;

namespace Million.Domain.Entities;

public class Property
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }
    public Address Address { get; private set; }   
    public Price Price { get; private set; }      
    public string CodeInternal { get; private set; } 
    public int Year { get; private set; }
    public Guid OwnerId { get; private set; }
    public List<PropertyTrace> PropertyTraces { get; private set; }
    public List<PropertyImage> PropertyImages { get; private set; }
    public PresentationConfig PresentationConfig { get; private set; }

    public Property(
        string name,
        Address address,
        Price price,
        Guid ownerId,
        string codeInternal,
        int year,
        List<PropertyImage> propertyImages,
        List<PropertyTrace> propertyTraces,
        PresentationConfig presentationConfig
    ) {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Property name is required", nameof(name));

        if (address is null)
            throw new ArgumentNullException(nameof(address), "Address is required");

        if (price is null)
            throw new ArgumentNullException(nameof(price), "Price is required");

        if (ownerId == Guid.Empty)
            throw new ArgumentException("OwnerId must be valid", nameof(ownerId));

        if (string.IsNullOrWhiteSpace(codeInternal))
            throw new ArgumentException("Internal code is required", nameof(codeInternal));

        if (year < 1800 || year > DateTime.UtcNow.Year)
            throw new ArgumentException("Year must be valid", nameof(year));
        if (propertyImages is null || propertyImages.Count == 0) 
            throw new ArgumentNullException(nameof(propertyImages), "Property must have images");
        if (presentationConfig is null)
            throw new ArgumentNullException(nameof(presentationConfig), "PresentationConfig is required");
        if (propertyTraces is null || propertyTraces.Count == 0)
            throw new ArgumentNullException(nameof(propertyTraces), "Property must have traces");
        
        Name = name;
        Address = address;
        Price = price;
        OwnerId = ownerId;
        CodeInternal = codeInternal;
        Year = year;
        PropertyTraces = propertyTraces;
        PropertyImages = propertyImages;
        PresentationConfig = presentationConfig;
    }
}