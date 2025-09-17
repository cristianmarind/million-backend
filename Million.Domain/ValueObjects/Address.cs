namespace Million.Domain.ValueObjects;

public class Address
{
    public string Street { get; }
    public string City { get; }
    public string Country { get; }

    public Address(string street, string city, string country)
    {
        if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException("Country is required");
        if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("City is required");
        if (string.IsNullOrWhiteSpace(street)) throw new ArgumentException("Street is required");
        
        Street = street;
        City = city;
        Country = country;
    }

}