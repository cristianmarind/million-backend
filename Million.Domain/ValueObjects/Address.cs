using Million.Domain.Exceptions;

namespace Million.Domain.ValueObjects;

public class Address
{
    public string Street { get; }
    public string City { get; }
    public string Country { get; }

    public Address(string street, string city, string country)
    {
        if (string.IsNullOrWhiteSpace(country)) throw new PropertyNotFoundException("Country is required");
        if (string.IsNullOrWhiteSpace(city)) throw new PropertyNotFoundException("City is required");
        if (string.IsNullOrWhiteSpace(street)) throw new PropertyNotFoundException("Street is required");
        /*
            Additional validations can be added here 
            1. Valid country names using a predefined list
            2. Valid city names using a predefined list
            3. Street format validation
            4. Valid city-country combinations
        */

        Street = street;
        City = city;
        Country = country;
    }

}